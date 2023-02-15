using Examen3;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace Empresa
{
    public partial class frmUsuario : Form
    {
        public frmUsuario()
        {
            InitializeComponent();
        }
        int Codigo = 0;
        double  ir, salb;
        Personal ll = new Personal();
        private void button1_Click(object sender, EventArgs e)
        {




            ll.Nombre = txtNombre.Text;
            ll.Profesion1 = cboProfesion.Text;
            ll.Sexo = cboSexo.Text;
            ll.Cédula1 = txtCedula.Text;
            ll.Estado = cboEstado.Text;
            ll.Salario = int.Parse(txtSalario.Text);
            ll.Cargo = txtCargo.Text;

            salb = int.Parse(txtSalario.Text);
            Codigo++;
           // txtCodigo.Text = Codigo.ToString();





            string[] datos = new string[8];


            datos[0] = txtNombre.Text;
            datos[1] = txtCedula.Text;
            datos[2] = cboSexo.Text;
            datos[3] = Codigo.ToString();
            datos[4] = cboProfesion.Text;
            datos[5] = ll.Salario.ToString();
            datos[6] = cboEstado.Text;
            datos[7] = ll.Cargo;

           // if (datos.Contains(ll.Cédula1))
            //{

             //   MessageBox.Show("No se puede repetir la cedula", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}


            Tabla.Rows.Add(datos);

            txtNombre.Text= String.Empty;
            cboProfesion.Text= String.Empty;
            cboSexo.Text = String.Empty;
            txtCedula.Text = String.Empty;
            cboEstado.Text = String.Empty;
            txtSalario.Text = String.Empty;
            txtCargo.Text = String.Empty;


        }

        

        private void txtNombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloLetras(e);
        }

        private void txtApellido_KeyPress(object sender, KeyPressEventArgs e)
        {
            Validar.SoloLetras(e);
        }

        private void frmUsuario_Load(object sender, EventArgs e)
        {
            Tabla.AllowUserToAddRows = false;
            
        }

        

       
        

        int strFila = 0;
        private void btnbuscar_Click(object sender, EventArgs e)
        {
            string buscar = txtBuscar.Text;
            if (txtBuscar.Text == "")
            {
                MessageBox.Show("No hay Datos");
            } else
            {
                foreach (DataGridViewRow Row in Tabla.Rows)
                 {       
                    strFila = Convert.ToInt32(Row.Index.ToString());
                    string valor = Convert.ToString(Row.Cells["ColumnaBuscada"].Value);

                    if (valor==buscar)
                    {
                        this.Tabla.CurrentCell = null;
                        int f = Tabla.RowCount;
                        for (int i = f - 1; i >= 0; i--)
                        {
                            this.Tabla.CurrentCell = null;
                            this.Tabla.Rows[i].Visible = false;
                            this.Tabla.Rows[strFila].Visible = true;
                        }

                        Tabla.Rows[strFila].DefaultCellStyle.BackColor = Color.Blue;
                    }
                
                }
            
                 }
        }

        public double Ir()
        {
            if (salb >0 && salb < 100000)
            {

               ir= 0;

            }
            if (salb >= 100000 && salb <= 200000) {


                ir = salb * 0.15;
            }

            if (salb >= 200000 && salb <= 350000) {

                ir = salb * 0.20;
            }
            if (salb >= 350000 && salb <= 500000)
            {
                ir = salb * 0.25;
            }
            if (salb >= 500000) {

                ir = salb * 0.30;
            }
            return ir;
        }
        double inss;
        double neto;
        public double Inss()
        {
            inss = salb * 0.065;

            return inss;
        }

        public double Base()
        {

            neto = salb - (Inss() + Ir());
            return neto;
        }

        private void btnExportar_Click(object sender, EventArgs e)
        {
            FileStream hc = new FileStream(@"F:\Programacion 1\Examen3\DocumentoGenerado.pdf", FileMode.Create);
            Document doc = new Document(PageSize.LETTER, 5 ,5 ,7 , 7);
            PdfWriter pw = PdfWriter.GetInstance(doc, hc);

            doc.Open();

            doc.AddAuthor("Alvaro");
            doc.AddTitle("PDF Generado");

            iTextSharp.text.Font standartfont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            doc.Add(new Paragraph("DocumentoGenerado"));
            doc.Add(Chunk.NEWLINE);

            PdfPTable Table = new PdfPTable(8);
            Table.WidthPercentage = 100;

            //Configurando el titulo de las columnas
            PdfPCell clNombre = new PdfPCell(new Phrase("Nombre", standartfont));
            clNombre.BorderWidth = 0;
            clNombre.BorderWidthBottom = 0.75f;

            PdfPCell clCedula = new PdfPCell(new Phrase("Cedula", standartfont));
            clCedula.BorderWidth = 0;
            clCedula.BorderWidthBottom = 0.75f;

            PdfPCell clSexo = new PdfPCell(new Phrase("Sexo", standartfont));
            clSexo.BorderWidth = 0;
            clSexo.BorderWidthBottom = 0.75f;

            PdfPCell clCodigo = new PdfPCell(new Phrase("Codigo", standartfont));
            clCodigo.BorderWidth = 0;
            clCodigo.BorderWidthBottom = 0.75f;

            PdfPCell clProfesion = new PdfPCell(new Phrase("Profesion", standartfont));
            clProfesion.BorderWidth = 0;
            clProfesion.BorderWidthBottom = 0.75f;

            PdfPCell clSalario = new PdfPCell(new Phrase("Salario", standartfont));
            clSalario.BorderWidth = 0;
            clSalario.BorderWidthBottom = 0.75f;

            PdfPCell clEstado = new PdfPCell(new Phrase("Estado", standartfont));
            clEstado.BorderWidth = 0;
            clEstado.BorderWidthBottom = 0.75f;

            PdfPCell clCargo = new PdfPCell(new Phrase("Cargo", standartfont));
            clCargo.BorderWidth = 0;
            clCargo.BorderWidthBottom = 0.75f;

            
            /*/
            foreach (var Trabajador in )
            {
                clNombre = new PdfPCell(new Phrase(Datos.Nombre, standarFont));
                clNombre.BorderWidth = 0;

                ClCedula = new PdfPCell(new Phrase(Datos.Cedula, standarFont));
                ClCedula.BorderWithd = 0;

                ClSexo = new PdfPCell(new Phrase(Datos.Nombre, standarFont));
                ClSexo.BorderWithd = 0;

                ClCodigo = new PdfPCell(new Phrase(Datos.Nombre, standarFont));
                ClCodigo.BorderWithd = 0;

                ClProfesion = new PdfPCell(new Phrase(Datos.Nombre, standarFont));
                ClProfesion.BorderWithd = 0;

                ClSalario = new PdfPCell(new Phrase(Datos.Nombre, standarFont));
                ClSalario.BorderWithd = 0;

                ClEstado = new PdfPCell(new Phrase(Datos.Nombre, standarFont));
                ClEstado.BorderWithd = 0;

                ClCargo = new PdfPCell(new Phrase(Datos.Nombre, standarFont));
                ClCargo.BorderWithd = 0;

                Tabla.AddCell(clNombre);
                Tabla.AddCell(clCedula);
                Tabla.AddCell(clSexo);
                Tabla.AddCell(clCodigo);
                Tabla.AddCell(clProfesion);
                Tabla.AddCell(clSalario);
                Tabla.AddCell(clEstado);
                Tabla.AddCell(clCargo);
            }

            doc.Add(Table);

            doc.Close();
            pw.Close();

            MessageBox.Show("Documento Generado Correctamente", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            /*/
        }   

        private void button2_Click(object sender, EventArgs e)
        {


            string [] datos2 = {ll.Nombre,ll.Apellido,ll.Salario.ToString(),ll.Profesion1,Base().ToString() };

            dtDeducciones.Rows.Add(datos2);
        }


      

    }


}
