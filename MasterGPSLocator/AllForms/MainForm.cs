using MasterGPSLocator.ProductTest;
using MasterGPSLocator.Tool;
using Production.Result;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Production.ProductionTest;

namespace MasterGPSLocator
{
    public partial class MainForm : Form
    {
        private Label lblSoftwareName;        //软件名称
        private Label lblProductModle;        //产品型号
        private Label lblCustomerInfo;        //客户信息
        private Label lblPlanCode;            //计划单号
        private bool isPressSpace;

        public bool IsPressSpace
        {
            get { return isPressSpace; }
            set { isPressSpace = value; }
        }

        public MainForm()
        {
            InitializeComponent();
            InitfrmMainHeader();

        }
        /*************************** 定义该类的自定义函数 ****************************/
        #region frmMainHeader
        /// <summary>
        /// 初始化主窗体头界面
        /// </summary>
        private void InitfrmMainHeader()
        {
            //初始化图片
            InitPicture();

            // 为创建的Label创建TableLayoutPanel布局控件
            TableLayoutPanel tlp = CreateTlp();

            //创建Label控件并添加集合
            List<Label> labelList = new List<Label>() {
                (lblSoftwareName = new Label()),
                (lblProductModle = new Label()),
                (lblCustomerInfo = new Label()),
                (lblPlanCode = new Label())
            };
            lblProductModle.Text = "产品型号："+ProductionInfo.ProductModel;
            lblCustomerInfo.Text = "客户信息："+ProductionInfo.CustomerName;


            // 初始化创建的Label
            InitCreatedLabel(labelList, tlp);


            txtLog.BackColor = Color.White;
            txtLog.ReadOnly = true;
        }

        /// <summary>
        /// 初始化图片
        /// </summary>
        private void InitPicture()
        {
            string resoucePath = "./Resouce/Picture/";
            picFormMainHeader.BackgroundImage = Image.FromFile(string.Format("{0}bg.jpg", resoucePath));
            picFormMainHeader.BackgroundImageLayout = ImageLayout.Stretch;

            //pictureBoxLogo
            //picLogo = new PictureBox();
            //picLogo.Name = "pictureBoxLogo";
            //picLogo.BackColor = Color.Transparent;
            //picLogo.Parent = picFormMainHeader;
            //picLogo.Location = new System.Drawing.Point(15, 10);
            //picLogo.Size = new System.Drawing.Size(picFormMainHeader.Size.Width - 30, 40);
            ////picLogo.BackgroundImage = null;
            ////picLogo.BackgroundImageLayout = ImageLayout.None;
            //picLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            //picLogo.Image = Image.FromFile(string.Format("{0}logo.png", resoucePath));
            ////picLogo.ImageLocation = string.Format("{0}logo_1.jpg", resoucePath);
            //picLogo.TabIndex = 01;
            //picLogo.TabStop = false;
        }

        /// <summary>
        /// 为创建的Label创建TableLayoutPanel布局控件
        /// </summary>
        /// <returns></returns>
        private TableLayoutPanel CreateTlp()
        {
            //tlp
            int xPosition = 40;
            TableLayoutPanel tlp = new TableLayoutPanel();
            tlp.Parent = picFormMainHeader;
            tlp.BackColor = Color.Transparent;
            tlp.Location = new Point(xPosition, 65);
            tlp.Size = new Size(picFormMainHeader.Size.Width - xPosition * 2, picFormMainHeader.Size.Height);
            tlp.ColumnCount = 1;
            tlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle
                (System.Windows.Forms.SizeType.Percent, 100F));

            return tlp;
        }

        /// <summary>
        /// 初始化创建的Label
        /// </summary>
        /// <param name="labelList"></param>
        /// <param name="parent"></param>
        private void InitCreatedLabel(List<Label> labelList, Control parent)
        {
            foreach (var label in labelList)
            {
                label.Parent = parent;
                label.AutoSize = true;
                label.Dock = DockStyle.Fill;
                label.BackColor = Color.Transparent;
                label.Font = new Font("黑体", 12F);
                label.ForeColor = Color.Black;
                label.Margin = new Padding(4, 4, 4, 4);
            }
        }

        /// <summary>
        /// 重初初始化主窗体头界面
        /// </summary>
        /// <param name="productModle"></param>
        /// <param name="customerInfo"></param>
        /// <param name="planCode"></param>
        public void ReInitfrmMainHeader(string productModle, string customerInfo, string planCode)
        {
            if (!this.InvokeRequired)
            {
                lblSoftwareName.Text = "C/N0检测";
                lblProductModle.Text = string.Format("产品型号：{0}", productModle);
                lblCustomerInfo.Text = string.Format("客户信息：{0}", customerInfo);
                lblPlanCode.Text = string.Format("计划单号：{0}", planCode);

            }
            else
            {
                Action<string, string, string> updateUI = (string productModle1, string customerInfo1,
                    string planCode1)
                      => ReInitfrmMainHeader(productModle1, customerInfo1, planCode1);
                //this.Invoke(updateUI, productModle, customerInfo, planCode);
                updateUI.Invoke(productModle, customerInfo, planCode);
            }
        }

        #endregion
        private void lblLabelImei_Click(object sender, EventArgs e)
        {
            txtLabelSn.Focus();
        }

        private void txtLabelSn_TextChanged(object sender, EventArgs e)
        {
            lblLabelSn.Visible = txtLabelSn.Text.Length < 1;
            int snLength = 12;
            if (txtLabelSn.Text.Length==snLength)
            {
                if (!Regex.IsMatch(txtLabelSn.Text,@"[0-9A-Z]{12}"))
                {
                    MessageBox.Show("MAC不合法");
                    ClearUILastTestState();
                    return;
                }
                //初始化测试状态
                txtLabelSn.ReadOnly = true;
                ClearUILastTestState();
                ProductTestFactory productionTestFactory = ProductTestFactory.GetProductTestFactory(this);

                //开启线程
                Thread thread = new Thread(productionTestFactory.CheckProductionTestState);
                thread.IsBackground = true;
                thread.Start(txtLabelSn.Text.Trim());
            }
            else if (txtLabelSn.Text.Length > snLength)
            {
                txtLabelSn.Text = txtLabelSn.Text.Substring(snLength);
                txtLabelSn.Select(txtLabelSn.Text.Length, 0);
            }

        }

        /// <summary>
        /// 读取日志
        /// </summary>
        /// <returns></returns>
        public string ReadLog()
        {
            string textRead = string.Empty;

            if (!this.InvokeRequired)
            {
                textRead = string.Format("{0}\r\n\r\n{1}", txtUart.Text, txtLog.Text);
            }
            else
            {
                Func<string> readUI = () => ReadLog();
                //textRead = readUI.Invoke();                   ???无线=限递归
                textRead = (string)this.Invoke(readUI);
            }

            return textRead;
        }

        /// <summary>
        /// 清除控件
        /// </summary>
        public void ClearUILastTestState()
        {
            //EnumControlWidget con = EnumControlWidget.txtLog;

            string name = EnumControlWidget.txtLog.ToString();
            SetText(name, string.Empty, false);
            SetTextColor(name, Color.White);


            name = EnumControlWidget.txtUart.ToString();
            SetText(name, string.Empty, false);

            //name = EnumControlWidget.txtLabelSn.ToString();
            //SetText(name, string.Empty, false);


        }
        /// <summary>
        /// 设置文本
        /// </summary>
        /// <param name="name"></param>
        /// <param name="text"></param>
        /// <param name="isAppend"></param>
        public void SetText(string name, string text, bool isAppend)
        {
            if (!this.InvokeRequired)
            {
                //Type t=this.GetType();
                //MessageBox.Show(t.ToString());//MasterGPSLocator.Form1
                //object obj=this.GetType().GetField()

                //FieldInfo[] fis = this.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);//BindingFlags.NonPublic|
                // foreach (FieldInfo fi in fis)
                //{
                //    MessageBox.Show(fi.Name);
                //    //Console.WriteLine(fi.Name);
                //}
                 FieldInfo f_info = this.GetType().GetField(name, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.IgnoreCase);
                 object obj = f_info.GetValue(this);//给定this对象中 支持的 该字段(txtLog)的值
                 if (obj is TextBox)
                 {
                     if (isAppend)
                     {
                         (obj as TextBox).AppendText(text);
                     }
                     else
                     {
                         (obj as TextBox).Text = text;
                     }
                 }
                 else if(obj is Label)
                 {
                     var labObj = obj as Label;
                     if (isAppend)
                     {
                         labObj.Text = text;
                     }
                     else
                     {
                         labObj.Text += text;
                     }
                 }
                 else
                 {
                     MessageBox.Show("控件{0}不是TextBox类型或Lable类型", name);
                 }

            }
            else
            {
                Action<string, string, bool> clearUI = (string name1,string text1,bool isAppend1) =>
                {
                    SetText(name1, text1, isAppend1);
                };
                this.Invoke(clearUI,name,text,isAppend);

            }
        }

        /// <summary>
        /// 设置文本背景颜色
        /// </summary>
        /// <param name="txtName"></param>
        /// <param name="color"></param>
        public void SetTextColor(string txtName, Color color)
        {
            if (!InvokeRequired)
            {
                object obj = this.GetType().GetField(txtName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.IgnoreCase).GetValue(this);
                if (obj !=null && obj is TextBox)
                {
                    var txtObj=obj as TextBox;
                    txtObj.BackColor = color;
                }
                //else
                //{
                //    MessageBox.Show(string.Format("控件:{0}不是TextBox类型",txtName));
                //}
            }
            else
            {
                Action<string, Color> clearColor = (string txtName1, Color color1) =>
                {
                    SetTextColor(txtName1, color1);
                };
                this.Invoke(clearColor, txtName, color);
            }
        }

        #region ReadOnly

        /// <summary>
        /// 设置文本框的只读
        /// </summary>
        /// <param name="txtName"></param>
        /// <param name="isReadonly"></param>
        public void SetTextBoxReadOnly(string txtName, bool isReadonly)
        {
            if (!this.InvokeRequired)
            {
                object obj = this.GetType().GetField(txtName, BindingFlags.NonPublic
                     | BindingFlags.Instance | BindingFlags.IgnoreCase).GetValue(this);
                if (obj != null && obj is TextBox)
                {
                    (obj as TextBox).ReadOnly = isReadonly;
                }

            }
            else
            {
                Action<string, bool> updateUI = (string txtName1, bool isReadonly1)
                     => SetTextBoxReadOnly(txtName1, isReadonly1);
                this.Invoke(updateUI, txtName, isReadonly);
                //updateUI.Invoke(txtName, isReadonly);
            }
        }

        #endregion

        /// <summary>
        /// 输出Log
        /// </summary>
        /// <param name="log"></param>
        public void DisplayLog(string log)
        {
            string name = EnumControlWidget.txtLog.ToString();
            log = System.DateTime.Now.ToString() + " "+log;
            SetText(name, log, true);
        }

        /// <summary>
        /// 输出UartLog
        /// </summary>
        /// <param name="log"></param>
        public void DisplayUart(string log)
        {
            string name = EnumControlWidget.txtUart.ToString();
            SetText(name, log, true);
        }
        /// <summary>
        /// 显示统计结果
        /// </summary>
        /// <param name="pass"></param>
        /// <param name="fail"></param>
        public void DisplayResultStatistics(int pass, int fail)
        {
            if (!this.InvokeRequired)
            {
                txtPass.Text = pass.ToString();
                txtFail.Text = fail.ToString();
                txtTotal.Text = (pass + fail).ToString();
                double yield = (pass + fail) == 0 ? 0 : (double)((double)pass / (double)(pass + fail));
                txtYeild.Text = yield.ToString("#0.00%");
            }
            else
            {
                Action<int, int> updateUI = (int pass1, int fail1) =>
                    DisplayResultStatistics(pass1, fail1);
                this.Invoke(updateUI, pass, fail);
                //updateUI.Invoke(pass, fail);
            }
        }

        /// <summary>
        /// 秒表显示
        /// </summary>
        /// <param name="span"></param>
        public void DisplayStopwatch(TimeSpan span)
        {
            if (!InvokeRequired)
            {
                DateTime dt = new DateTime(span.Ticks);
                lblStopWatch.Text = dt.ToString("HH:mm:ss:fff");
            }
            else
            {
                Action<TimeSpan> updateTime = (TimeSpan span1) =>
                {
                    DisplayStopwatch(span1);
                };
                this.Invoke(updateTime, span);
            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("是否清零？", "清零", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.OK)
            {
                ResultInfo.ClearResult();

                DisplayResultStatistics(0, 0);

                txtLabelSn.Text = string.Empty;
                //txtSn.Text = string.Empty;

                txtUart.BackColor = Color.White;
                txtUart.Text = string.Empty;
                txtLog.BackColor = Color.White;
                txtLog.Text = string.Empty;
                //txtLog.Text = "正在检测模块上电，请插入模块...\r\n";
                lblStopWatch.Text = "00:00:00:000";
            }
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            //初始化结果
            DisplayResultStatistics(ResultInfo.Pass, ResultInfo.Fail);


        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                IsPressSpace = true;
            }
            
        }




    }
}
