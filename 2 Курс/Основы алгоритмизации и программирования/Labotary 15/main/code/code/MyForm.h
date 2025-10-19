#pragma once
#include<cmath>

namespace code {

	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

	/// <summary>
	/// Summary for MyForm
	/// </summary>
	public ref class MyForm : public System::Windows::Forms::Form
	{
	public:
		MyForm(void)
		{
			InitializeComponent();
			//
			//TODO: Add the constructor code here
			//
		}

	protected:
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		~MyForm()
		{
			if (components)
			{
				delete components;
			}
		}

	private:
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System::ComponentModel::Container ^components;
		System::Windows::Forms::GroupBox^ groupBox1;
		System::Windows::Forms::RadioButton^ TenPercentSpace;
		System::Windows::Forms::RadioButton^ Perimetr;
	private: System::Windows::Forms::Label^ label1;
	private: System::Windows::Forms::Label^ label2;
	private: System::Windows::Forms::TextBox^ Width;
	private: System::Windows::Forms::TextBox^ Heigth;


	private: System::Windows::Forms::Label^ label3;
	private: System::Windows::Forms::TextBox^ Result;

	private: System::Windows::Forms::Button^ button1;
	private: System::Windows::Forms::CheckBox^ DoubleValue;

		   System::Windows::Forms::RadioButton^ Space;

#pragma region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		void InitializeComponent(void)
		{
			this->TenPercentSpace = (gcnew System::Windows::Forms::RadioButton());
			this->Perimetr = (gcnew System::Windows::Forms::RadioButton());
			this->Space = (gcnew System::Windows::Forms::RadioButton());
			this->groupBox1 = (gcnew System::Windows::Forms::GroupBox());
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->label2 = (gcnew System::Windows::Forms::Label());
			this->Width = (gcnew System::Windows::Forms::TextBox());
			this->Heigth = (gcnew System::Windows::Forms::TextBox());
			this->label3 = (gcnew System::Windows::Forms::Label());
			this->Result = (gcnew System::Windows::Forms::TextBox());
			this->button1 = (gcnew System::Windows::Forms::Button());
			this->DoubleValue = (gcnew System::Windows::Forms::CheckBox());
			this->groupBox1->SuspendLayout();
			this->SuspendLayout();
			// 
			// TenPercentSpace
			// 
			this->TenPercentSpace->AutoSize = true;
			this->TenPercentSpace->Location = System::Drawing::Point(35, 71);
			this->TenPercentSpace->Name = L"TenPercentSpace";
			this->TenPercentSpace->Size = System::Drawing::Size(139, 24);
			this->TenPercentSpace->TabIndex = 1;
			this->TenPercentSpace->TabStop = true;
			this->TenPercentSpace->Text = L"10%Площади";
			this->TenPercentSpace->UseVisualStyleBackColor = true;
			this->TenPercentSpace->CheckedChanged += gcnew System::EventHandler(this, &MyForm::radioButton_CheckedChanged);
			// 
			// Perimetr
			// 
			this->Perimetr->AutoSize = true;
			this->Perimetr->Location = System::Drawing::Point(35, 114);
			this->Perimetr->Name = L"Perimetr";
			this->Perimetr->Size = System::Drawing::Size(111, 24);
			this->Perimetr->TabIndex = 2;
			this->Perimetr->TabStop = true;
			this->Perimetr->Text = L"Периметр";
			this->Perimetr->UseVisualStyleBackColor = true;
			this->Perimetr->CheckedChanged += gcnew System::EventHandler(this, &MyForm::radioButton_CheckedChanged);
			// 
			// Space
			// 
			this->Space->AutoSize = true;
			this->Space->Location = System::Drawing::Point(35, 28);
			this->Space->Name = L"Space";
			this->Space->Size = System::Drawing::Size(107, 24);
			this->Space->TabIndex = 0;
			this->Space->TabStop = true;
			this->Space->Text = L"Площадь";
			this->Space->UseVisualStyleBackColor = true;
			this->Space->CheckedChanged += gcnew System::EventHandler(this, &MyForm::radioButton_CheckedChanged);
			// 
			// groupBox1
			// 
			this->groupBox1->BackColor = System::Drawing::Color::LightGray;
			this->groupBox1->Controls->Add(this->Space);
			this->groupBox1->Controls->Add(this->Perimetr);
			this->groupBox1->Controls->Add(this->TenPercentSpace);
			this->groupBox1->Location = System::Drawing::Point(84, 185);
			this->groupBox1->Name = L"groupBox1";
			this->groupBox1->Size = System::Drawing::Size(200, 160);
			this->groupBox1->TabIndex = 6;
			this->groupBox1->TabStop = false;
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Location = System::Drawing::Point(53, 49);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(196, 24);
			this->label1->TabIndex = 3;
			this->label1->Text = L"Ширина прямоугольника";
			this->label1->UseCompatibleTextRendering = true;
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->Location = System::Drawing::Point(53, 96);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(194, 20);
			this->label2->TabIndex = 7;
			this->label2->Text = L"Высота прямоугольника";
			// 
			// Width
			// 
			this->Width->Location = System::Drawing::Point(254, 49);
			this->Width->Name = L"Width";
			this->Width->Size = System::Drawing::Size(128, 26);
			this->Width->TabIndex = 8;
			this->Width->KeyPress += gcnew System::Windows::Forms::KeyPressEventHandler(this, &MyForm::Parameters_KeyPress1);
			// 
			// Heigth
			// 
			this->Heigth->Location = System::Drawing::Point(254, 90);
			this->Heigth->Name = L"Heigth";
			this->Heigth->Size = System::Drawing::Size(128, 26);
			this->Heigth->TabIndex = 9;
			this->Heigth->KeyPress += gcnew System::Windows::Forms::KeyPressEventHandler(this, &MyForm::Parameters_KeyPress2);
			// 
			// label3
			// 
			this->label3->AutoSize = true;
			this->label3->Location = System::Drawing::Point(399, 185);
			this->label3->Name = L"label3";
			this->label3->Size = System::Drawing::Size(83, 20);
			this->label3->TabIndex = 10;
			this->label3->Text = L"Значение";
			// 
			// Result
			// 
			this->Result->Location = System::Drawing::Point(374, 213);
			this->Result->Name = L"Result";
			this->Result->Size = System::Drawing::Size(207, 26);
			this->Result->TabIndex = 11;
			// 
			// button1
			// 
			this->button1->Location = System::Drawing::Point(375, 256);
			this->button1->Name = L"button1";
			this->button1->Size = System::Drawing::Size(127, 40);
			this->button1->TabIndex = 12;
			this->button1->Text = L"Вычислить";
			this->button1->UseVisualStyleBackColor = true;
			this->button1->Click += gcnew System::EventHandler(this, &MyForm::ResultButton_Click);
			// 
			// DoubleValue
			// 
			this->DoubleValue->AutoSize = true;
			this->DoubleValue->Location = System::Drawing::Point(84, 369);
			this->DoubleValue->Name = L"DoubleValue";
			this->DoubleValue->Size = System::Drawing::Size(194, 24);
			this->DoubleValue->TabIndex = 13;
			this->DoubleValue->Text = L"Удвоенное значение";
			this->DoubleValue->UseVisualStyleBackColor = true;
			// 
			// MyForm
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(9, 20);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(639, 439);
			this->Controls->Add(this->DoubleValue);
			this->Controls->Add(this->button1);
			this->Controls->Add(this->Result);
			this->Controls->Add(this->label3);
			this->Controls->Add(this->Heigth);
			this->Controls->Add(this->Width);
			this->Controls->Add(this->label2);
			this->Controls->Add(this->label1);
			this->Controls->Add(this->groupBox1);
			this->Name = L"MyForm";
			this->Text = L"Расчёт площади и периметра";
			this->groupBox1->ResumeLayout(false);
			this->groupBox1->PerformLayout();
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion
		int option = 0;
		private: System::Void radioButton_CheckedChanged(System::Object^ sender, System::EventArgs^ e) {
			if (Space->Checked == true)
			{
				option = 0;
			}
			else if (Perimetr->Checked == true)
			{
				option = 1;
			}
			else if (TenPercentSpace->Checked == true) {
				option = 2;
			}
		}
		private: System::Void ResultButton_Click(System::Object^ sender, System::EventArgs^ e) {
			double value = 0.0;
			try {
				switch (option)
				{
				case 0:
					value = System::Convert::ToDouble(Width->Text) * System::Convert::ToDouble(Heigth->Text);
					break;
				case 1:
					value = System::Convert::ToDouble(Width->Text) * 2 + System::Convert::ToDouble(Heigth->Text) * 2;
					break;
				case 2:
					value = System::Convert::ToDouble(Width->Text) * System::Convert::ToDouble(Heigth->Text) * 0.1;
					break;

				default:
					break;
				}
				value = DoubleValue->Checked ? value * 2 : value;
				value = std::round(value * 1000) / 1000;
				if (value == 0) {
					Result->Text = "Прямоугольника не существует";
				}
				else {
					Result->Text = value.ToString();
				}
			}
			catch (System::Exception^ e) {
				MessageBox::Show("Ошибка - требуется ввод: " + e->Message, "Error");
			}
		}
		private:
			System::Void Parameters_KeyPress1(System::Object^ sender, System::Windows::Forms::KeyPressEventArgs^ e)
			{
				if ((e->KeyChar >= '0') && (e->KeyChar <= '9'))
					return;

				if (e->KeyChar == '.')
					e->KeyChar = ',';

				if (e->KeyChar == ',') {
					if ((Width->Text->IndexOf(',') != -1) ||
						(Width->Text->Length == 0)) {
						e->Handled = true;
					}
					return;
				}
				if (Char::IsControl(e->KeyChar)) {
					if (e->KeyChar == (char)Keys::Enter) {
						Heigth->Focus();
					}
					return;
				}
				e->Handled = true;
			}

			System::Void Parameters_KeyPress2(System::Object^ sender, System::Windows::Forms::KeyPressEventArgs^ e)
			{
				if ((e->KeyChar >= '0') && (e->KeyChar <= '9'))
					return;

				if (e->KeyChar == '.')
					e->KeyChar = ',';

				if (e->KeyChar == ',') {
					if ((Heigth->Text->IndexOf(',') != -1) ||
						(Heigth->Text->Length == 0)) {
						e->Handled = true;
					}
					return;
				}
				if (Char::IsControl(e->KeyChar)) {
					if (e->KeyChar == (char)Keys::Enter) {
						Width->Focus();
					}
					return;
				}
				e->Handled = true;
			}
};
}
