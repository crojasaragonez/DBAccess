using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Implementation;

namespace UIFileReader
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            this.CargarImp();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string imp = this.cmbImp.Text;
            string file = this.txtArchivo.Text;

            var types = this.types();

            foreach (var type in types)
            {
                IReader implementation = this.getInstanceByType(type);
                if (implementation.Name().Equals(imp)) {
                    string result = implementation.Read(file);
                    this.txtContent.Text = result;
                }
            }
        }

        private void CargarImp() {
            var types = this.types();
            foreach (var imp in types)
            {
                IReader implementation = this.getInstanceByType(imp);
                this.cmbImp.Items.Add(implementation.Name());
            }
        }

        private IReader getInstanceByType(Type type) {
            return (IReader)Activator.CreateInstance(type);
        }

        private IEnumerable<Type> types()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(s => s.GetTypes())
                    .Where(p => typeof(IReader).IsAssignableFrom(p))
                    .Where(a => !a.FullName.Equals("Implementation.IReader"));
        }
    }
}
