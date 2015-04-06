using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace WLOracleProxy
{
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();
        }

        private void btRun_Click(object sender, EventArgs e)
        {
            getConfigFromUI();
            startThread();
        }

        private void getConfigFromUI()
        {
            btRun.Enabled = false;
        }

        private void iceThread()
        {
            int status = 0;
            Ice.Communicator ic = null;

            try
            {
                // ic = Ice.Util.initialize(ref args);
                ic = Ice.Util.initialize();
                Ice.ObjectAdapter adapter = ic.createObjectAdapterWithEndpoints("DBFunc", "tcp -p 10086");
                Ice.Object obj = new DBFuncI();
                adapter.add(obj, ic.stringToIdentity("dbfunc"));
                adapter.activate();
                ic.waitForShutdown();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.Error.WriteLine(e);
                status = 1;
            }

            if (ic != null)
            {
                try
                {
                    ic.destroy();
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e);
                    status = 1;
                }
            }

            Environment.Exit(status);
        }


        private void startThread()
        {
            ThreadStart ts = new ThreadStart(iceThread);
            Thread t = new Thread(ts);
            t.IsBackground = true;
            t.Start();
        }
    }
}
