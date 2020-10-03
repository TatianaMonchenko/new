using System;
using System.CodeDom;
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

namespace Calculator
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string leftop = "";
        string operation = "";
        string rightop = "";

        public MainWindow()
        {
            InitializeComponent();
            foreach (UIElement c in LayoutRoot.Children)
            {
                if (c is Button)
                {
                    ((Button)c).Click += Button_Click;
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            string s = (string)((Button)e.OriginalSource).Content;
            
            textBox.Text += s;
            int num;
            
            bool result = Int32.TryParse(s, out num);
           
            if (result == true)
            {
                
                if (operation == "")
                {
                    
                    leftop += s;
                }
                else
                {
                  
                    rightop += s;
                }
            }
            
            else
            {
                
                if (s == "=")
                {
                    Update_RightOp();
                    textBox.Text += rightop;
                    operation = "";
                }
                
                else if (s == "CLEAR")
                {
                    leftop = "";
                    rightop = "";
                    operation = "";
                    textBox.Text = "";
                }
                
                else
                {
                    
                    if (rightop != "")
                    {
                        Update_RightOp();
                        leftop = rightop;
                        rightop = "";
                    }
                    operation = s;
                }
            }
        }
        
        private void Update_RightOp()
        {
            int num1 = Int32.Parse(leftop);
            int num2 = Int32.Parse(rightop);
           
            switch (operation)
            {
                case "+":
                    rightop = (num1 + num2).ToString();
                    break;
                case "-":
                    rightop = (num1 - num2).ToString();
                    break;
                case "*":
                    rightop = (num1 * num2).ToString();
                    break;
                case "/":
                    rightop = (num1 / num2).ToString();
                    break;
            }
        }
    }
}

//private void Button_Click(object sender, RoutedEventArgs e)
//{
//    var value = (char)(sender as Button).Content.ToString().First();
//    if (value >= '0' && value <= '9')
//    {

//    }
//    else
//    {

//    }
//}
