using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _8bitVonNeiman.Core.View;

namespace _8bitVonNeiman.Core {
    public class CentralController: ApplicationContext, ComponentsFormOutput {

        private ComponentsForm _componentsForm;

        public CentralController() {
            _componentsForm = new ComponentsForm(this);
            _componentsForm.Show();
        }

        public void FormClosed() {
            ExitThread();
        }

        public void EditorButtonClicked() {
            
        }
    }
}
