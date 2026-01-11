#include "MyForm.h"
#include <cmath>

using namespace System;
using namespace System::Windows::Forms;

[STAThreadAttribute]
int main(array<String^>^ args)
{
    Application::EnableVisualStyles();
    Application::SetCompatibleTextRenderingDefault(false);
    code::MyForm form;
    Application::Run(% form);
    return 0;
}