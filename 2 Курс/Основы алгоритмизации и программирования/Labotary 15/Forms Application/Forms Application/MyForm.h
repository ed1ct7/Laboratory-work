#pragma once

namespace FormsApplication {

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
	private: System::Windows::Forms::Label^ label1;
	protected:
	private: System::Windows::Forms::Label^ label2;
	private: System::Windows::Forms::Label^ label3;
	private: System::Windows::Forms::Label^ label4;
	private: System::Windows::Forms::TextBox^ textBox1;
	private: System::Windows::Forms::TextBox^ textBox2;
	private: System::Windows::Forms::Button^ button1;

	protected:

	private:
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System::ComponentModel::Container ^components;

#pragma region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		void InitializeComponent(void)
		{
			this->label1 = (gcnew System::Windows::Forms::Label());
			this->label2 = (gcnew System::Windows::Forms::Label());
			this->label3 = (gcnew System::Windows::Forms::Label());
			this->label4 = (gcnew System::Windows::Forms::Label());
			this->textBox1 = (gcnew System::Windows::Forms::TextBox());
			this->textBox2 = (gcnew System::Windows::Forms::TextBox());
			this->button1 = (gcnew System::Windows::Forms::Button());
			this->SuspendLayout();
			// 
			// label1
			// 
			this->label1->AutoSize = true;
			this->label1->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 16, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label1->Location = System::Drawing::Point(98, 96);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(367, 37);
			this->label1->TabIndex = 0;
			this->label1->Text = L"моя первая программа";
			// 
			// label2
			// 
			this->label2->AutoSize = true;
			this->label2->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 11, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label2->Location = System::Drawing::Point(114, 178);
			this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(151, 26);
			this->label2->TabIndex = 1;
			this->label2->Text = L"Сумма (руб.):";
			// 
			// label3
			// 
			this->label3->AutoSize = true;
			this->label3->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 11, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label3->Location = System::Drawing::Point(114, 236);
			this->label3->Name = L"label3";
			this->label3->Size = System::Drawing::Size(128, 26);
			this->label3->TabIndex = 2;
			this->label3->Text = L"Срок (мес.)";
			// 
			// label4
			// 
			this->label4->AutoSize = true;
			this->label4->Font = (gcnew System::Drawing::Font(L"Microsoft Sans Serif", 11, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point,
				static_cast<System::Byte>(204)));
			this->label4->Location = System::Drawing::Point(114, 299);
			this->label4->Name = L"label4";
			this->label4->Size = System::Drawing::Size(0, 26);
			this->label4->TabIndex = 3;
			// 
			// textBox1
			// 
			this->textBox1->Location = System::Drawing::Point(290, 178);
			this->textBox1->Name = L"textBox1";
			this->textBox1->Size = System::Drawing::Size(189, 26);
			this->textBox1->TabIndex = 4;
			this->textBox1->TextChanged += gcnew System::EventHandler(this, &MyForm::textBox1_TextChanged);
			this->textBox1->KeyPress += gcnew System::Windows::Forms::KeyPressEventHandler(this, &MyForm::textBox1_KeyPress_1);
			// 
			// textBox2
			// 
			this->textBox2->Location = System::Drawing::Point(290, 236);
			this->textBox2->Name = L"textBox2";
			this->textBox2->Size = System::Drawing::Size(189, 26);
			this->textBox2->TabIndex = 5;
			this->textBox2->TextChanged += gcnew System::EventHandler(this, &MyForm::textBox2_TextChanged);
			this->textBox2->KeyPress += gcnew System::Windows::Forms::KeyPressEventHandler(this, &MyForm::textBox2_KeyPress_1);
			// 
			// button1
			// 
			this->button1->Location = System::Drawing::Point(359, 372);
			this->button1->Name = L"button1";
			this->button1->Size = System::Drawing::Size(120, 48);
			this->button1->TabIndex = 6;
			this->button1->Text = L"Расчёт";
			this->button1->UseVisualStyleBackColor = true;
			this->button1->Click += gcnew System::EventHandler(this, &MyForm::button1_Click);
			// 
			// MyForm
			// 
			this->AutoScaleDimensions = System::Drawing::SizeF(9, 20);
			this->AutoScaleMode = System::Windows::Forms::AutoScaleMode::Font;
			this->ClientSize = System::Drawing::Size(600, 463);
			this->Controls->Add(this->button1);
			this->Controls->Add(this->textBox2);
			this->Controls->Add(this->textBox1);
			this->Controls->Add(this->label4);
			this->Controls->Add(this->label3);
			this->Controls->Add(this->label2);
			this->Controls->Add(this->label1);
			this->Name = L"MyForm";
			this->Text = L"MyForm";
			this->ResumeLayout(false);
			this->PerformLayout();

		}
#pragma endregion
	private: System::Void button1_Click(System::Object^ sender, System::EventArgs^ e) {
		double sum, percent, profit; //сумма вклада, процентная ставка, срок
		int period; //срок

		sum = System::Convert::ToDouble(textBox1->Text);
		period = System::Convert::ToInt32(textBox2->Text);

		sum < 10000 ? percent = 8.5 : percent = 12;

		profit = sum * (percent / 100 / 12) * period;

		label4->Text =
			"Процентная ставка: " + percent.ToString("n") + "%\n" +
			"Доход: " + profit.ToString("C");
	}

	private: System::Void textBox1_TextChanged(System::Object^ sender, System::EventArgs^ e)
	{
		label4->Text = "";
		if ((textBox1->Text->Length == 0) ||
			(textBox2->Text->Length == 0))
			button1->Enabled = false;
		else
			button1->Enabled = true;
	}

	private: System::Void textBox1_KeyPress_1(System::Object^ sender, 
		System::Windows::Forms::KeyPressEventArgs^ e) {
		if ((e->KeyChar >= '0') && (e->KeyChar <= '9'))
			return;

		if (e->KeyChar == '.')
			e->KeyChar = ',';

		if (e->KeyChar == ',') {
			if ((textBox1->Text->IndexOf(',') != -1) || 
				(textBox1->Text->Length == 0)) {
				e->Handled = true;
			}
			return;
		}
		if (Char::IsControl(e->KeyChar)) {
			if (e->KeyChar == (char)Keys::Enter) {
				textBox2->Focus();
			}
			return;
		}
		e->Handled = true;
	}

	private: System::Void textBox2_TextChanged(System::Object^ sender, System::EventArgs^ e) {
		textBox1_TextChanged(sender, e);
	}

	private: System::Void textBox2_KeyPress_1(System::Object^ sender, System::Windows::Forms::KeyPressEventArgs^ e) {
		if ((e->KeyChar >= '0') && (e->KeyChar <= '9'))
			return;
		if (Char::IsControl(e->KeyChar)) {
			if (e->KeyChar == (char)Keys::Enter) {
				button1->Focus();
			}
			return;
		}
		e->Handled = true;
	}
};
}






