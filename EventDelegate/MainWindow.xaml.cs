using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace EventDelegate
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// 猫看见了一只偷吃的老鼠叫了一声，吓跑了老鼠，吵醒了主人，
    /// 主人生气了去抓老鼠，老鼠躲进了洞里，猫躲进了被子里
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<BaseInformation> baseInformation = new ObservableCollection<BaseInformation>()
        {
                new Host("ren"){Type="人" },
                new Cat("mao") {Type="猫" },
                new Mouse("shu"){Type="鼠" },
        };
        Cat cat;
        public MainWindow()
        {
            InitializeComponent();

            lb.ItemsSource = baseInformation;
            cat = baseInformation.ToList().Find((f) => f.Type.Equals("猫")) as Cat;
            Mouse mouse = baseInformation.ToList().Find((f) => f.Type.Equals("鼠")) as Mouse;
            Host host = baseInformation.ToList().Find((f) => f.Type.Equals("人")) as Host; ;
            cat.猫叫Event += new Cat.CatEventHandler(mouse.跑了);
            cat.猫叫Event += new Cat.CatEventHandler(host.睡醒);
            host.睡醒Event += new Host.HostEventHandler(host.生气);
            host.生气Event += new Host.HostEventHandler(mouse.躲进洞里);
            host.生气Event += new Host.HostEventHandler(cat.躲进被窝);
        }

     
        private void Bt_Click(object sender, RoutedEventArgs e)
        {                      
            cat.猫叫();
            this.tb.Text = EventMessage.Message;          
        }
        private void Bt_Click2(object sender, RoutedEventArgs e)
        {
            Cat cat =new Cat() { Name="x"} ;
            Mouse mouse = new Mouse() { Name="y"};
            Host host = new Host() { Name="z"};
            cat.猫叫Event += new Cat.CatEventHandler(mouse.跑了);
            cat.猫叫Event += new Cat.CatEventHandler(host.睡醒);
            host.睡醒Event += new Host.HostEventHandler(host.生气);
            host.生气Event += new Host.HostEventHandler(mouse.躲进洞里);
            host.生气Event += new Host.HostEventHandler(cat.躲进被窝);

            cat.猫叫();
            this.tb.Text = EventMessage.Message;
        }
    }



    public class Cat : BaseInformation
    {
        public Cat() { }


        public Cat(string name)
        {
            this.Name = name;
            this.Type = "猫";
        }

        public delegate void CatEventHandler(string value);
        public event CatEventHandler 猫叫Event;


        public void 猫叫()
        {
            S = string.Format("名字叫{0}的猫叫了一声", Name);
            EventMessage.Message = S;
            this.猫叫Event?.Invoke(S);
            
        }
        public void 躲进被窝(string value)
        {
            S =  string.Format("名字叫{0}的猫躲进了被窝", Name);
            value = value + S;
            EventMessage.Message = EventMessage.Message + Environment.NewLine + S;
        }
    }

    public class Host : BaseInformation
    {
        public Host() { }

        public Host(string name)
        {
            this.Name = name;
            this.Type = "人";
        }

        public delegate void HostEventHandler(string value);
        public event HostEventHandler 睡醒Event;
        public event HostEventHandler 生气Event;

        public void 睡醒(string value)
        {
            S = string.Format("名字叫{0}的人被吵醒", Name);
            value = value + Environment.NewLine + S;
            EventMessage.Message = EventMessage.Message + Environment.NewLine + S;
            this.睡醒Event?.Invoke(value);
            
        }

        public void 生气(string value)
        {
            S = string.Format("名字叫{0}的人生气了", Name);
            value = value + Environment.NewLine + S;
            EventMessage.Message = EventMessage.Message + Environment.NewLine + S;
            this.生气Event.Invoke(value);
            
        }
    }
    public class Mouse : BaseInformation
    {
        public Mouse(string name)
        {
            this.Name = name;
            this.Type = "老鼠";
        }

        public Mouse() { }

        public delegate void MouseEventHandler(string value);     

        public void 跑了(string value)
        {
            S = string.Format("名字叫{0}的老鼠跑了", Name);
            value = value + Environment.NewLine + S;
            EventMessage.Message = EventMessage.Message+Environment.NewLine+S;
        }

        public void 躲进洞里(string value)
        {
            S = string.Format("名字叫{0}的老鼠躲进了洞里", Name);
            value = value + S;
            EventMessage.Message = EventMessage.Message + Environment.NewLine + S;

        }

    }


    public class EventMessage
    {
     public static  string Message { get; set; }
    }

    public class BaseInformation
    {
        public string S { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }


        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
    }
}
