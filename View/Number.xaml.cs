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
using System.Windows.Shapes;
using Sanator.ModelDb;
using Sanator.Tools;

namespace Sanator.View
{
    /// <summary>
    /// Interaction logic for Number.xaml
    /// </summary>
    public partial class Number : Window
    {
        public Number()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TypeNumber t = new TypeNumber();
            t.Show();
        }

        
        public Number EditNumber { get; set; }
        public  CommandVM SaveNumber { get; set; }
        //private void Init()
        //{

        //    SaveNumber = new CommandVM(() => {

        //        EditNumber.EditNumber = EditNumber.ID;
        //        var model = SqlModel.GetInstance();
        //        if (EditNumber.ID == 0)
        //            model.Insert(EditNumber);
        //        else
        //            model.Update(EditNumber);
                
        //    });
        //}
    }
}
