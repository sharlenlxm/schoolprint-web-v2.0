using System;
using System.Net;//网络功能 
using System.IO;//流支持
using System.Threading;//线程支持
using System.Windows.Forms;

namespace SchoolPrint
{
    public class DownloadUtil
    {
        public int threadcount;//下载文件线程的数量
        public string fileUrl;//下载文件的远程地址URL
        public string filename;//保存到本地的文件名称

        private AutoResetEvent[] EventS;//创建AutoResetEvent对象(多线程调度指令)
        private Thread DownFileMain;//下载文件主线程
        private Thread[] DownFiles;//下载文件数据块线程
        private MsgClass msgClass;

        public DownloadUtil(string url, string name, int count,MsgClass msgclass)
        {
            msgClass = msgclass;
            fileUrl = url;
            FileInfo file = new FileInfo(name);
            if (Directory.Exists(file.DirectoryName))
                Directory.CreateDirectory(file.DirectoryName);
            filename = name + ".lu";//没有下载完成前文件的后缀名为“.lu”
            threadcount = count;
            DownFiles = new Thread[threadcount];
            msgClass.ThreadCount = threadcount;//信息数据类线程个数
            EventS = new AutoResetEvent[threadcount];
            for (int i = 0; i < threadcount; i++)
                EventS[i] = new AutoResetEvent(false);
        }
        public void Suspend()//暂停文件下载
        {
            for (int i = 0; i < threadcount; i++)
            {
                try
                {
                    if (DownFiles[i].IsAlive) DownFiles[i].Suspend();//abore是执行以后抛出一个异常
                }
                catch
                {
                    ;
                }
            }
            try
            {
                if (DownFileMain.IsAlive) DownFileMain.Suspend();
            }
            catch
            {
                ;
            }
            
            msgClass.Msg = "下载文件暂停。";
        }

        public void Resume()//恢复文件下载
        {
            for (int i = 0; i < threadcount; i++)
            {
                try
                {
                    if (DownFiles[i].IsAlive) DownFiles[i].Resume();//abore是执行以后抛出一个异常
                }
                catch
                {
                    ;
                }
            }
            try
            {
                if (DownFileMain.IsAlive) DownFileMain.Resume();
            }
            catch
            {
                ;
            }

            msgClass.Msg = threadcount.ToString() + " 个线程正在下载文件。";
        }

        public void Abort()
        {
            for (int i = 0; i < threadcount; i++)
            {
                if (DownFiles[i].IsAlive) DownFiles[i].Abort();//abore是执行以后抛出一个异常
            }
            if (DownFileMain.IsAlive) DownFileMain.Abort();
            msgClass.Msg = "下载文件暂停。";
        }

        public void downfile()//多线程下载文件
        {
            ServicePointManager.DefaultConnectionLimit = 25;//默认情况下，System.Net 对每个主机的每个应用程序使用两个连接。至少要大于线程数。但：初始化后改变此属性没有影响
            //ServicePointManager.DefaultConnectionLimit = threadcount;这里必须大于线程数
            DownFileMain = new Thread(new ThreadStart(getdownfile));//获取文件大小、多线程下载多文件段
            DownFileMain.Start();
        }
        private void getdownfile()//获取文件大小、多线程下载多文件段
        {
            try
            {
                msgClass.Msg = "获取下载文件的大小。";//信息
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(fileUrl);//需要下载的文件全路径
                int filesize = (int)request.GetResponse().ContentLength;//取得下载文件的大小
                msgClass.FileSize = filesize;
                request.Abort();
                int fileonethreadsize = filesize / threadcount;//平均分配
                FileStream WriteFS = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);//生成保存的文件
                WriteFS.Write(new byte[filesize], 0, filesize);//用空内容填充
                WriteFS.Close();
                msgClass.Msg = threadcount.ToString() + " 个线程正在下载文件。";//信息
                msgClass.StartTime = DateTime.Now;
                for (int i = 0; i < threadcount; i++)
                {
                    threadreceive tr;
                    if (i == threadcount - 1)//最后一个下载线程
                        tr = new threadreceive(i, fileonethreadsize * i, filesize, fileUrl, filename,msgClass,EventS);
                    else
                        tr = new threadreceive(i, fileonethreadsize * i, fileonethreadsize * i + fileonethreadsize - 1, fileUrl, filename,msgClass,EventS);
                    DownFiles[i] = new Thread(new ThreadStart(tr.receive));//启动接收数据块线程组（threadcount个）
                    DownFiles[i].Start();
                }
                WaitHandle.WaitAll(EventS);//等待所有线程都结束的信号
                int temp = 0;
                for (int i = 0; i < msgClass.ThreadCount; i++)
                    temp += msgClass.threadsistrue[i];
                if (temp == msgClass.ThreadCount)//所有线程下载成功
                {
                    if (File.Exists(filename.Substring(0, filename.Length - 3))) File.Delete(filename.Substring(0, filename.Length - 3));//下载的文件已经存在，则先删除
                    //for (int k = 0; !File.Exists(filename.Substring(0, filename.Length - 3)); k++)
                    //{
                    //    filename = k.ToString() + filename;
                    //}
                    File.Move(filename, filename.Substring(0, filename.Length - 3));//文件下载完成后更名，去掉后缀名“.lu”。
                    msgClass.Msg = "下载文件完成。";//信息
                }
                else
                    msgClass.Msg = "下载文件失败。";//信息
            }
            catch (ThreadAbortException)
            {
                msgClass.Msg = "下载文件取消。";//信息
                msgClass.ThreadCount = 0;
                msgClass.FileSize = 0;
            }
            catch (Exception Mye)
            {
                msgClass.Msg = Mye.Message;//信息
            }
        }
        private class threadreceive//线程下载文件类
        {
            private int threadIndex;//线程号
            private int from, to;//文件下载的开始和结束位置
            private string fileUrl;//下载文件的远程地址URL
            private string filename;//保存到本地的文件名称
            private MsgClass msgClass;
            private AutoResetEvent[] EventS;
            public threadreceive(int index, int f, int t, string Url, string Name, MsgClass msgclass, AutoResetEvent[] eventS)
            {
                EventS = eventS;
                threadIndex = index;
                from = f;
                to = t;  
                fileUrl = Url;
                filename = Name; 
                msgClass = msgclass;
            }
            public void receive()//接收线程
            {
                FileStream WriteFS = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);//打开保存的文件
                WriteFS.Seek(from, SeekOrigin.Begin);//文件定位于当前线程写内容的位置
                byte[] nbytes = new byte[1024 * 10];
                try
                {
                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(fileUrl);//有时网络不正常会连接不上，这里可以作多次连接
                    request.AddRange(from, to);//接收的起始位置及接收的结束位置 
                    Stream ns = request.GetResponse().GetResponseStream();//获得接收流
                    int nreadsize;
                    while ((nreadsize = ns.Read(nbytes, 0, nbytes.Length)) > 0)
                    {
                        WriteFS.Write(nbytes, 0, nreadsize);
                        msgClass.threadsdata[threadIndex] += nreadsize;//单个线程总共下载数据量
                    }
                    ns.Close();
                    msgClass.threadsistrue[threadIndex] = 1;//此线程下载数据块成功
                }
                catch { }
                WriteFS.Close();
                EventS[threadIndex].Set();//发送本线程已经结束的信号
            }
        }
    }
    /// <summary>
    /// 多线程下载文件，线程之间信息数据共享类
    /// </summary>
    public class MsgClass
    {
        private static int threadcount;//线程数量

        //public static int[] threadsdata;//线程下载文件完成量
        //public static int[] threadsistrue;//线程下载文件是否完全成功
        //public static string Msg = "下载文件开始。";
        //public static int FileSize;//下载文件大小
        //public static DateTime StartTime;//开始下载时间

        private int[] Threadsdata;//线程下载文件完成量
        private int[] Threadsistrue;//线程下载文件是否完全成功
        private string msg = "下载文件开始。";
        private int fileSize;//下载文件大小
        private DateTime startTime;//开始下载时间

        public int ThreadCount
        {
            get
            {
                return threadcount;
            }
            set
            {
                threadcount = value;
                threadsdata = new int[threadcount];
                for (int i = 0; i < threadcount; i++)
                    threadsdata[i] = 0;
                threadsistrue = new int[threadcount];
                for (int i = 0; i < threadcount; i++)
                    threadsistrue[i] = 0;
            }
        }
        public string Msg
        {
            get
            {
                return msg;
            }
            set
            {
                msg = value;
            }
        }
        public int FileSize
        {
            get
            {
                return fileSize;
            }
            set
            {
                fileSize = value;
            }
        }
        public DateTime StartTime
        {
            get
            {
                return startTime;
            }
            set
            {
                startTime = value;
            }
        }
        public int[] threadsdata
        {
            get 
            {
                return Threadsdata;
            }
            set
            {
                Threadsdata=value;
            }
        }
        public int[] threadsistrue
        {
            get
            {
                return Threadsistrue;
            }
            set
            {
                Threadsistrue = value;
            }
        }
    }
}

