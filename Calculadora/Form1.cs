using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculadora
{
    public partial class frmCalculadora : Form
    {   //Os cálculos são sempre baseados em dois números: Um número que foi apresentado antes de apertar um botão de operação, e o outro número apresentado depois de apertar um botão de operação
        string visorNumero = "";  //Captar os números pressionados pelo usuário
        decimal primeiroNumero;   //Captar o o primeiro número de uma operação
        string parteInteira = "";  //Caso se use a vírgula, captar a parte que representa a parte inteira do número
        string parteDecimal = "";  //Caso se use a vírgula, captar a parte que representa a parte decimal do número
        string operacao = "";  //Captar a operação selecionada
        bool novoNumero = true;  //Informar se o número na tela pode ser sobreposto
        bool segundoNumero = false;  //Informar se o segundo valor de uma operação foi informado
        bool visorResultado = false;  //Informar se o visor estiver mostrando o resultado de uma operação com o botão de igual
        bool visorResultado2 = false; //Informar se o visor estiver mostrando o resultado de uma operação com o botão de 1/x, raiz quadrada ou elevar ao quadrado. Também é usado para indicar quando o botão de porcentagem é apertado
        bool calculoInvalido = false; //Informar quando estiver exibindo uma mensagem de erro


        public frmCalculadora()
        {
            InitializeComponent();
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            if (novoNumero == true || visorResultado2 == true)                                          //A inserção de números é dividida em duas partes. A primeira consiste na inserção do primeiro digito do número  
            {
                if (visorResultado == true)
                {
                    txtVisorCalculo.Clear();
                    visorResultado = false;
                }

                if (calculoInvalido == true)
                {
                    txtVisorCalculo.Clear();
                    calculoInvalido = false;
                }

              
                if (visorResultado2 == true)           //Tratamento dos caracteres do visor de cálculo quando o número zero substitui o resultado de um cálculo relacionado à variável visorResultado2
                {
                    if (segundoNumero == false)
                    {
                        txtVisorCalculo.Text = "0";
                    }
                    else
                    {
                        if (primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 16 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 18)
                        {
                            txtVisorCalculo.Text = primeiroNumero.ToString("e") + operacao + "0";
                        }
                        else
                        {
                            txtVisorCalculo.Text = primeiroNumero.ToString() + operacao + "0";
                        }
                    }

                    visorResultado2 = false;
                }                    
                else if (txtVisorCalculo.Text != "0" && txtVisorCalculo.Text != "0" + operacao + "0")     //Tratamento dos caracteres do visor de cálculo para evitar á inserção de números zero indevidos
                {
                    txtVisorCalculo.Text += "0";
                }

                txtVisor.Clear();
                visorNumero = "0";
                txtVisor.Text = visorNumero;

                if (operacao != "")                                //Quando um número é inserido depois de pressionar um botão de operação ele é tomado como o segundo número de um cálculo
                {
                    segundoNumero = true;
                }
            }
            else if (visorNumero.Contains(',') == false && visorNumero.Contains('-') == false && visorNumero != "0" && visorNumero.Length <= 15 || visorNumero.Contains(',') == false && visorNumero.Contains('-') == true && visorNumero != "0" && visorNumero.Length <= 16 || visorNumero.Contains(',') == true && visorNumero.Contains('-') == false && parteInteira.Length + parteDecimal.Length <= 16 || visorNumero.Contains(',') == true && visorNumero.Contains('-') == true && parteInteira.Length + parteDecimal.Length <= 17)                      //A segunda parte consiste na inserção de mais digitos caso assim for desejado. Essa divisão no processo de inserção auxilia em duas situações.
            {                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                //Uma situação em que se deve substituir o número que está na tela, e em outra situação em que se deve adicionar números ao número que estão na tela. 
                txtVisorCalculo.Text += "0";                           

                if (visorNumero.Contains(','))                                            //Caso uma vírgula seja inserida, o número será armazenado em duas variáveis: Uma variável para os digitos antes da vírgula, e outra variável para os dígitos depois da vírgula
                {                                                                        //Essa divisão auxilia na formatação da exibição dos números no visor, e também auxilia na identificação e remoção de casas decimais de valor nulo quando se aperta um botão de operação ou o botão de igual
                    if (visorNumero != parteInteira)                                    //Só deve ser possível inserir números com no máximo 16 dígitos  
                    {
                        parteInteira = visorNumero;
                    }                                         
                    
                    parteDecimal += "0";                                               
                    
                    if (visorNumero.StartsWith("0") || visorNumero.StartsWith("-0"))    //Caso a parte inteira do número seja apenas um número zero, o número é exibido sem qualquer formatação pois a formatação identifica esse zero como supérfluo, omitindo esse número na exibição do visor
                    {
                        txtVisor.Text = parteInteira + parteDecimal;
                    }
                    else                                                                                                    //Nos demais casos, se exibe com formatação apenas os caracteres da variável que capta os números antes da vírgula
                    {                                                                                                      //Essa divisão ocorre pois a parte decimal com formatação faz com que algumas vezes a exibição da inserção de números zeros não ocorra pois eles podem ser interpretados como supérfluos
                        txtVisor.Text = Convert.ToDecimal(parteInteira).ToString("###,###") + ',' + parteDecimal;                                                                                      
                    }
                }
                else
                {
                    visorNumero += "0";
                    txtVisor.Text = Convert.ToDecimal(visorNumero).ToString("###,###");
                }
            }
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            if (novoNumero == true || visorResultado2 == true)
            {
                if (visorResultado == true)
                {
                    txtVisorCalculo.Clear();
                    visorResultado = false;
                }

                if (calculoInvalido == true)
                {
                    txtVisorCalculo.Clear();
                    calculoInvalido = false;
                }

                       
                if (visorNumero == "0" && txtVisorCalculo.Text.EndsWith("0"))                                         //Caso o número zero esteja selecionado para ser um número de um cálculo, mas depois se queira substitui-lo por um outro número, ele é apagado do visor que mostra o cálculo 
                {
                    txtVisorCalculo.Text = txtVisorCalculo.Text.Remove(txtVisorCalculo.Text.Length - 1);
                    txtVisorCalculo.Text += "1";
                }
                else
                {
                    if (visorResultado2 == true)
                    {
                        if (segundoNumero == false)
                        {
                            txtVisorCalculo.Text = "1";
                        }
                        else
                        {
                            if (primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 16 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 18)
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString("e") + operacao + "1";
                            }
                            else
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString() + operacao + "1";
                            }
                        }

                        visorResultado2 = false;
                    }
                    else
                    {
                       txtVisorCalculo.Text += "1";
                    } 
                }
                
                txtVisor.Clear();
                visorNumero = "1";
                txtVisor.Text = visorNumero;
                
                novoNumero = false;

                if (operacao != "")
                {
                    segundoNumero = true;
                }
            }
             else if (visorNumero.Contains(',') == false && visorNumero.Contains('-') == false && visorNumero.Length <= 15 || visorNumero.Contains(',') == false && visorNumero.Contains('-') == true && visorNumero.Length <= 16 || visorNumero.Contains(',') == true && visorNumero.Contains('-') == false && parteInteira.Length + parteDecimal.Length <= 16 || visorNumero.Contains(',') == true && visorNumero.Contains('-') == true && parteInteira.Length + parteDecimal.Length <= 17)
             {
                txtVisorCalculo.Text += "1";

                if (visorNumero.Contains(','))
                {
                    if (visorNumero != parteInteira)
                    {
                        parteInteira = visorNumero;
                    }
                    
                    parteDecimal += "1";

                    if (visorNumero.StartsWith("0") || visorNumero.StartsWith("-0"))
                    {
                        txtVisor.Text = parteInteira + parteDecimal;
                    }
                    else
                    { 
                        txtVisor.Text = Convert.ToDecimal(parteInteira).ToString("###,###") + ',' + parteDecimal;
                    }

                }
                else
                {
                    visorNumero += "1";
                    txtVisor.Text = Convert.ToDecimal(visorNumero).ToString("###,###");
                }
            }
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            if (novoNumero == true || visorResultado2 == true)
            {
                if (visorResultado == true)
                {
                    txtVisorCalculo.Clear();
                    visorResultado = false;
                }

                if (calculoInvalido == true)
                {
                    txtVisorCalculo.Clear();
                    calculoInvalido = false;
                }

                if (visorNumero == "0" && txtVisorCalculo.Text.EndsWith("0"))                                       
                {
                    txtVisorCalculo.Text = txtVisorCalculo.Text.Remove(txtVisorCalculo.Text.Length - 1);
                    txtVisorCalculo.Text += "2";
                }
                else
                {
                    if (visorResultado2 == true)
                    {
                        if (segundoNumero == false)
                        {
                            txtVisorCalculo.Text = "2";
                        }
                        else
                        {
                            if (primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 16 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 18)
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString("e") + operacao + "2";
                            }
                            else
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString() + operacao + "2";
                            }
                        }

                        visorResultado2 = false;
                    }
                    else
                    {
                        txtVisorCalculo.Text += "2";
                    }
                }

                txtVisor.Clear();
                visorNumero = "2";
                txtVisor.Text = visorNumero;

                novoNumero = false;

                if (operacao != "")
                {
                    segundoNumero = true;
                }
            }
            else if (visorNumero.Contains(',') == false && visorNumero.Contains('-') == false && visorNumero.Length <= 15 || visorNumero.Contains(',') == false && visorNumero.Contains('-') == true && visorNumero.Length <= 16 || visorNumero.Contains(',') == true && visorNumero.Contains('-') == false && parteInteira.Length + parteDecimal.Length <= 16 || visorNumero.Contains(',') == true && visorNumero.Contains('-') == true && parteInteira.Length + parteDecimal.Length <= 17)
            {
                txtVisorCalculo.Text += "2";

                if (visorNumero.Contains(','))
                {
                    if (visorNumero != parteInteira)
                    {
                        parteInteira = visorNumero;
                    }

                    parteDecimal += "2";

                    if (visorNumero.StartsWith("0") || visorNumero.StartsWith("-0"))
                    {
                        txtVisor.Text = parteInteira + parteDecimal;
                    }
                    else
                    {
                        txtVisor.Text = Convert.ToDecimal(parteInteira).ToString("###,###") + ',' + parteDecimal;
                    }

                }
                else
                {
                    visorNumero += "2";
                    txtVisor.Text = Convert.ToDecimal(visorNumero).ToString("###,###");
                }
            }
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            if (novoNumero == true || visorResultado2 == true)
            {
                if (visorResultado == true)
                {
                    txtVisorCalculo.Clear();
                    visorResultado = false;
                }

                if (calculoInvalido == true)
                {
                    txtVisorCalculo.Clear();
                    calculoInvalido = false;
                }

                if (visorNumero == "0" && txtVisorCalculo.Text.EndsWith("0"))
                {
                    txtVisorCalculo.Text = txtVisorCalculo.Text.Remove(txtVisorCalculo.Text.Length - 1);
                    txtVisorCalculo.Text += "2";
                }
                else
                {
                    if (visorResultado2 == true)
                    {
                        if (segundoNumero == false)
                        {
                            txtVisorCalculo.Text = "3";
                        }
                        else
                        {
                            if (primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 16 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 18)
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString("e") + operacao + "3";
                            }
                            else
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString() + operacao + "3";
                            }
                        }

                        visorResultado2 = false;
                    }
                    else
                    {
                        txtVisorCalculo.Text += "3";
                    }
                }

                txtVisor.Clear();
                visorNumero = "3";
                txtVisor.Text = visorNumero;

                novoNumero = false;

                if (operacao != "")
                {
                    segundoNumero = true;
                }
            }
            else if (visorNumero.Contains(',') == false && visorNumero.Contains('-') == false && visorNumero.Length <= 15 || visorNumero.Contains(',') == false && visorNumero.Contains('-') == true && visorNumero.Length <= 16 || visorNumero.Contains(',') == true && visorNumero.Contains('-') == false && parteInteira.Length + parteDecimal.Length <= 16 || visorNumero.Contains(',') == true && visorNumero.Contains('-') == true && parteInteira.Length + parteDecimal.Length <= 17)
            {
                txtVisorCalculo.Text += "3";

                if (visorNumero.Contains(','))
                {
                    if (visorNumero != parteInteira)
                    {
                        parteInteira = visorNumero;
                    }

                    parteDecimal += "3";

                    if (visorNumero.StartsWith("0") || visorNumero.StartsWith("-0"))
                    {
                        txtVisor.Text = parteInteira + parteDecimal;
                    }
                    else
                    {
                        txtVisor.Text = Convert.ToDecimal(parteInteira).ToString("###,###") + ',' + parteDecimal;
                    }

                }
                else
                {
                    visorNumero += "3";
                    txtVisor.Text = Convert.ToDecimal(visorNumero).ToString("###,###");
                }
            }
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            if (novoNumero == true || visorResultado2 == true)
            {
                if (visorResultado == true)
                {
                    txtVisorCalculo.Clear();
                    visorResultado = false;
                }

                if (calculoInvalido == true)
                {
                    txtVisorCalculo.Clear();
                    calculoInvalido = false;
                }

                if (visorNumero == "0" && txtVisorCalculo.Text.EndsWith("0"))
                {
                    txtVisorCalculo.Text = txtVisorCalculo.Text.Remove(txtVisorCalculo.Text.Length - 1);
                    txtVisorCalculo.Text += "4";
                }
                else
                {
                    if (visorResultado2 == true)
                    {
                        if (segundoNumero == false)
                        {
                            txtVisorCalculo.Text = "4";
                        }
                        else
                        {
                            if (primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 16 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 18)
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString("e") + operacao + "4";
                            }
                            else
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString() + operacao + "4";
                            }
                        }

                        visorResultado2 = false;
                    }
                    else
                    {
                        txtVisorCalculo.Text += "4";
                    }
                }

                txtVisor.Clear();
                visorNumero = "4";
                txtVisor.Text = visorNumero;

                novoNumero = false;

                if (operacao != "")
                {
                    segundoNumero = true;
                }
            }
            else if (visorNumero.Contains(',') == false && visorNumero.Contains('-') == false && visorNumero.Length <= 15 || visorNumero.Contains(',') == false && visorNumero.Contains('-') == true && visorNumero.Length <= 16 || visorNumero.Contains(',') == true && visorNumero.Contains('-') == false && parteInteira.Length + parteDecimal.Length <= 16 || visorNumero.Contains(',') == true && visorNumero.Contains('-') == true && parteInteira.Length + parteDecimal.Length <= 17)
            {
                txtVisorCalculo.Text += "4";

                if (visorNumero.Contains(','))
                {
                    if (visorNumero != parteInteira)
                    {
                        parteInteira = visorNumero;
                    }

                    parteDecimal += "4";

                    if (visorNumero.StartsWith("0") || visorNumero.StartsWith("-0"))
                    {
                        txtVisor.Text = parteInteira + parteDecimal;
                    }
                    else
                    {
                        txtVisor.Text = Convert.ToDecimal(parteInteira).ToString("###,###") + ',' + parteDecimal;
                    }

                }
                else
                {
                    visorNumero += "4";
                    txtVisor.Text = Convert.ToDecimal(visorNumero).ToString("###,###");
                }
            }
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            if (novoNumero == true || visorResultado2 == true)
            {
                if (visorResultado == true)
                {
                    txtVisorCalculo.Clear();
                    visorResultado = false;
                }

                if (calculoInvalido == true)
                {
                    txtVisorCalculo.Clear();
                    calculoInvalido = false;
                }

                if (visorNumero == "0" && txtVisorCalculo.Text.EndsWith("0"))
                {
                    txtVisorCalculo.Text = txtVisorCalculo.Text.Remove(txtVisorCalculo.Text.Length - 1);
                    txtVisorCalculo.Text += "5";
                }
                else
                {
                    if (visorResultado2 == true)
                    {
                        if (segundoNumero == false)
                        {
                            txtVisorCalculo.Text = "5";
                        }
                        else
                        {
                            if (primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 16 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 18)
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString("e") + operacao + "5";
                            }
                            else
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString() + operacao + "5";
                            }
                        }

                        visorResultado2 = false;
                    }
                    else
                    {
                        txtVisorCalculo.Text += "5";
                    }
                }

                txtVisor.Clear();
                visorNumero = "5";
                txtVisor.Text = visorNumero;

                novoNumero = false;

                if (operacao != "")
                {
                    segundoNumero = true;
                }
            }
            else if (visorNumero.Contains(',') == false && visorNumero.Contains('-') == false && visorNumero.Length <= 15 || visorNumero.Contains(',') == false && visorNumero.Contains('-') == true && visorNumero.Length <= 16 || visorNumero.Contains(',') == true && visorNumero.Contains('-') == false && parteInteira.Length + parteDecimal.Length <= 16 || visorNumero.Contains(',') == true && visorNumero.Contains('-') == true && parteInteira.Length + parteDecimal.Length <= 17)
            {
                txtVisorCalculo.Text += "5";

                if (visorNumero.Contains(','))
                {
                    if (visorNumero != parteInteira)
                    {
                        parteInteira = visorNumero;
                    }

                    parteDecimal += "5";

                    if (visorNumero.StartsWith("0") || visorNumero.StartsWith("-0"))
                    {
                        txtVisor.Text = parteInteira + parteDecimal;
                    }
                    else
                    {
                        txtVisor.Text = Convert.ToDecimal(parteInteira).ToString("###,###") + ',' + parteDecimal;
                    }

                }
                else
                {
                    visorNumero += "5";
                    txtVisor.Text = Convert.ToDecimal(visorNumero).ToString("###,###");
                }
            }
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            if (novoNumero == true || visorResultado2 == true)
            {
                if (visorResultado == true)
                {
                    txtVisorCalculo.Clear();
                    visorResultado = false;
                }

                if (calculoInvalido == true)
                {
                    txtVisorCalculo.Clear();
                    calculoInvalido = false;
                }

                if (visorNumero == "0" && txtVisorCalculo.Text.EndsWith("0"))
                {
                    txtVisorCalculo.Text = txtVisorCalculo.Text.Remove(txtVisorCalculo.Text.Length - 1);
                    txtVisorCalculo.Text += "6";
                }
                else
                {
                    if (visorResultado2 == true)
                    {
                        if (segundoNumero == false)
                        {
                            txtVisorCalculo.Text = "6";
                        }
                        else
                        {
                            if (primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 16 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 18)
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString("e") + operacao + "6";
                            }
                            else
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString() + operacao + "6";
                            }
                        }

                        visorResultado2 = false;
                    }
                    else
                    {
                        txtVisorCalculo.Text += "6";
                    }
                }

                txtVisor.Clear();
                visorNumero = "6";
                txtVisor.Text = visorNumero;

                novoNumero = false;

                if (operacao != "")
                {
                    segundoNumero = true;
                }
            }
            else if (visorNumero.Contains(',') == false && visorNumero.Contains('-') == false && visorNumero.Length <= 15 || visorNumero.Contains(',') == false && visorNumero.Contains('-') == true && visorNumero.Length <= 16 || visorNumero.Contains(',') == true && visorNumero.Contains('-') == false && parteInteira.Length + parteDecimal.Length <= 16 || visorNumero.Contains(',') == true && visorNumero.Contains('-') == true && parteInteira.Length + parteDecimal.Length <= 17)
            {
                txtVisorCalculo.Text += "6";

                if (visorNumero.Contains(','))
                {
                    if (visorNumero != parteInteira)
                    {
                        parteInteira = visorNumero;
                    }

                    parteDecimal += "6";

                    if (visorNumero.StartsWith("0") || visorNumero.StartsWith("-0"))
                    {
                        txtVisor.Text = parteInteira + parteDecimal;
                    }
                    else
                    {
                        txtVisor.Text = Convert.ToDecimal(parteInteira).ToString("###,###") + ',' + parteDecimal;
                    }
                }
                else
                {
                    visorNumero += "6";
                    txtVisor.Text = Convert.ToDecimal(visorNumero).ToString("###,###");
                }
            }
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            if (novoNumero == true || visorResultado2 == true)
            {
                if (visorResultado == true)
                {
                    txtVisorCalculo.Clear();
                    visorResultado = false;
                }

                if (calculoInvalido == true)
                {
                    txtVisorCalculo.Clear();
                    calculoInvalido = false;
                }

                if (visorNumero == "0" && txtVisorCalculo.Text.EndsWith("0"))
                {
                    txtVisorCalculo.Text = txtVisorCalculo.Text.Remove(txtVisorCalculo.Text.Length - 1);
                    txtVisorCalculo.Text += "7";
                }
                else
                {
                    if (visorResultado2 == true)
                    {
                        if (segundoNumero == false)
                        {
                            txtVisorCalculo.Text = "7";
                        }
                        else
                        {
                            if (primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 16 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 18)
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString("e") + operacao + "7";
                            }
                            else
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString() + operacao + "7";
                            }
                        }

                        visorResultado2 = false;
                    }
                    else
                    {
                        txtVisorCalculo.Text += "7";
                    }
                }

                txtVisor.Clear();
                visorNumero = "7";
                txtVisor.Text = visorNumero;

                novoNumero = false;

                if (operacao != "")
                {
                    segundoNumero = true;
                }
            }
            else if (visorNumero.Contains(',') == false && visorNumero.Contains('-') == false && visorNumero.Length <= 15 || visorNumero.Contains(',') == false && visorNumero.Contains('-') == true && visorNumero.Length <= 16 || visorNumero.Contains(',') == true && visorNumero.Contains('-') == false && parteInteira.Length + parteDecimal.Length <= 16 || visorNumero.Contains(',') == true && visorNumero.Contains('-') == true && parteInteira.Length + parteDecimal.Length <= 17)
            {
                txtVisorCalculo.Text += "7";

                if (visorNumero.Contains(','))
                {
                    if (visorNumero != parteInteira)
                    {
                        parteInteira = visorNumero;
                    }

                    parteDecimal += "7";

                    if (visorNumero.StartsWith("0") || visorNumero.StartsWith("-0"))
                    {
                        txtVisor.Text = parteInteira + parteDecimal;
                    }
                    else
                    {
                        txtVisor.Text = Convert.ToDecimal(parteInteira).ToString("###,###") + ',' + parteDecimal;
                    }

                }
                else
                {
                    visorNumero += "7";
                    txtVisor.Text = Convert.ToDecimal(visorNumero).ToString("###,###");
                }
            }
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            if (novoNumero == true || visorResultado2 == true)
            {
                if (visorResultado == true)
                {
                    txtVisorCalculo.Clear();
                    visorResultado = false;
                }

                if (calculoInvalido == true)
                {
                    txtVisorCalculo.Clear();
                    calculoInvalido = false;
                }

                if (visorNumero == "0" && txtVisorCalculo.Text.EndsWith("0"))
                {
                    txtVisorCalculo.Text = txtVisorCalculo.Text.Remove(txtVisorCalculo.Text.Length - 1);
                    txtVisorCalculo.Text += "8";
                }
                else
                {
                    if (visorResultado2 == true)
                    {
                        if (segundoNumero == false)
                        {
                            txtVisorCalculo.Text = "8";
                        }
                        else
                        {
                            if (primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 16 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 18)
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString("e") + operacao + "8";
                            }
                            else
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString() + operacao + "8";
                            }
                        }

                        visorResultado2 = false;
                    }
                    else
                    {
                        txtVisorCalculo.Text += "8";
                    }
                }

                txtVisor.Clear();
                visorNumero = "8";
                txtVisor.Text = visorNumero;

                novoNumero = false;

                if (operacao != "")
                {
                    segundoNumero = true;
                }
            }
            else if (visorNumero.Contains(',') == false && visorNumero.Contains('-') == false && visorNumero.Length <= 15 || visorNumero.Contains(',') == false && visorNumero.Contains('-') == true && visorNumero.Length <= 16 || visorNumero.Contains(',') == true && visorNumero.Contains('-') == false && parteInteira.Length + parteDecimal.Length <= 16 || visorNumero.Contains(',') == true && visorNumero.Contains('-') == true && parteInteira.Length + parteDecimal.Length <= 17)
            {
                txtVisorCalculo.Text += "8";

                if (visorNumero.Contains(','))
                {
                    if (visorNumero != parteInteira)
                    {
                        parteInteira = visorNumero;
                    }

                    parteDecimal += "8";

                    if (visorNumero.StartsWith("0") || visorNumero.StartsWith("-0"))
                    {
                        txtVisor.Text = parteInteira + parteDecimal;
                    }
                    else
                    {
                        txtVisor.Text = Convert.ToDecimal(parteInteira).ToString("###,###") + ',' + parteDecimal;
                    }

                }
                else
                {
                    visorNumero += "8";
                    txtVisor.Text = Convert.ToDecimal(visorNumero).ToString("###,###");
                }
            }
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            if (novoNumero == true || visorResultado2 == true)
            {
                if (visorResultado == true)
                {
                    txtVisorCalculo.Clear();
                    visorResultado = false;
                }

                if (calculoInvalido == true)
                {
                    txtVisorCalculo.Clear();
                    calculoInvalido = false;
                }

                if (visorNumero == "0" && txtVisorCalculo.Text.EndsWith("0"))
                {
                    txtVisorCalculo.Text = txtVisorCalculo.Text.Remove(txtVisorCalculo.Text.Length - 1);
                    txtVisorCalculo.Text += "9";
                }
                else
                {
                    if (visorResultado2 == true)
                    {
                        if (segundoNumero == false)
                        {
                            txtVisorCalculo.Text = "9";
                        }
                        else
                        {
                            if (primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 16 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 18)
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString("e") + operacao + "9";
                            }
                            else
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString() + operacao + "9";
                            }
                        }

                        visorResultado2 = false;
                    }
                    else
                    {
                        txtVisorCalculo.Text += "9";
                    }
                }

                txtVisor.Clear();
                visorNumero = "9";
                txtVisor.Text = visorNumero;

                novoNumero = false;

                if (operacao != "")
                {
                    segundoNumero = true;
                }
            }
            else if (visorNumero.Contains(',') == false && visorNumero.Contains('-') == false && visorNumero.Length <= 15 || visorNumero.Contains(',') == false && visorNumero.Contains('-') == true && visorNumero.Length <= 16 || visorNumero.Contains(',') == true && visorNumero.Contains('-') == false && parteInteira.Length + parteDecimal.Length <= 16 || visorNumero.Contains(',') == true && visorNumero.Contains('-') == true && parteInteira.Length + parteDecimal.Length <= 17)
            {
                txtVisorCalculo.Text += "9";

                if (visorNumero.Contains(','))
                {
                    if (visorNumero != parteInteira)
                    {
                        parteInteira = visorNumero;
                    }

                    parteDecimal += "9";

                    if (visorNumero.StartsWith("0") || visorNumero.StartsWith("-0"))
                    {
                        txtVisor.Text = parteInteira + parteDecimal;
                    }
                    else
                    {
                        txtVisor.Text = Convert.ToDecimal(parteInteira).ToString("###,###") + ',' + parteDecimal;
                    }

                }
                else
                {
                    visorNumero += "9";
                    txtVisor.Text = Convert.ToDecimal(visorNumero).ToString("###,###");
                }
            }
        }

        private void btnVirgula_Click(object sender, EventArgs e)
        {
            if (visorNumero.Contains(',') == false && visorNumero.Length <= 15 && visorResultado2 == false)
            {
                if (visorNumero == "")        //Inserção da vírgula quando o número 0 for o número 0 inicial ou alcançado pelo uso do botão de backspace
                {
                    visorNumero = "0,";
                    txtVisor.Text = visorNumero;
                    txtVisorCalculo.Text = visorNumero;

                    novoNumero = false;
                }
                else if (visorNumero == "0" && visorResultado == false)    //Inserção da vírgula quando o número 0 for introduzido pelo botão número )
                {
                    visorNumero += ',';
                    txtVisor.Text = visorNumero;
                    txtVisorCalculo.Text += ",";

                    novoNumero = false;
                }
                else if (novoNumero == false)
                {
                    visorNumero += ',';
                    txtVisor.Text = Convert.ToDecimal(visorNumero).ToString("###,###") + ',';
                    txtVisorCalculo.Text += ",";
                }
            }
        }

        private void btnAdicao_Click(object sender, EventArgs e)
        {
            if (segundoNumero == false && calculoInvalido == false)                                             //Procedimentos ao se clicar em um botão de operação depois de informar o primeiro número de um cálculo
            {
                if (novoNumero == false)
                {
                    novoNumero = true;
                }

                if (visorResultado2 == true)
                {
                    visorResultado2 = false;
                }

                if (operacao != "+")
                {
                    operacao = "+";
                }


                if (txtVisorCalculo.Text.EndsWith("+") || txtVisorCalculo.Text.EndsWith("-") || txtVisorCalculo.Text.EndsWith("x") || txtVisorCalculo.Text.EndsWith("/"))    //Primeiro se verifica se o usuário pressionou botões de operação seguidamente. Caso seja esse caso, A nova operação substitui a operação que foi informada anteriormente
                {
                    txtVisorCalculo.Text = txtVisorCalculo.Text.Remove(txtVisorCalculo.Text.Length - 1);

                    txtVisorCalculo.Text += "+";
                }
                else if (visorNumero == "")                                     //Caso se aperte um botão de operação quando o visor estiver com o número zero inicial, ou o número zero seja introduzido pelo backspace, esse número será tomado como o primeiro número de um cálculo
                {
                    txtVisorCalculo.Text = "0+";
                    visorNumero = "0";
                    primeiroNumero = Convert.ToDecimal(visorNumero);      //Ao se apertar um botão de operação, o primeiro número de um cálculo é definido
                }
                else if (visorResultado == true)                         //Caso se aperte um botão de operação quando o visor estiver mostrando o resultado de um cálculo, esse resultado será tomado como o primeiro número de um próximo cálculo
                {
                    if (visorNumero.ToString().Contains(',') == false && visorNumero.ToString().Contains('-') == false && visorNumero.ToString().Length > 16 || visorNumero.ToString().Contains(',') == false && visorNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 17 || visorNumero.ToString().Contains(',') == true && visorNumero.ToString().Contains('-') == false && visorNumero.ToString().Length > 17 || visorNumero.ToString().Contains(',') == true && visorNumero.ToString().Contains('-') == true && visorNumero.ToString().Length > 18)
                    {
                        txtVisorCalculo.Text = Convert.ToDecimal(visorNumero).ToString("e") + "+";
                    }
                    else
                    {
                        txtVisorCalculo.Text = visorNumero + "+";
                    }
                    
                    primeiroNumero = Convert.ToDecimal(visorNumero);
                    visorResultado = false;
                }
                else 
                {
                    if (visorNumero.Contains(','))                                                                           //Procedimentos caso o número informado tenha vírgula. Todas as condições são feitas de uma maneira para também evitar que, caso o número com vírgula for introduzido pelo botão 1/x, raiz quadrada, elevar ao quadrado, ou for o resultado de um cálculo, ele caia nessas estruturas condicionais
                    {
                        if (visorNumero.EndsWith(",") == true && parteDecimal == "")                                         //Caso tenha-se colocado uma vírgula no número, mas não tenha-se colocado números na casa decimal, ao se apertar um botão de operação essa vírgula será removida
                        {
                                visorNumero = visorNumero.Remove(visorNumero.Length - 1);

                                if (visorNumero.StartsWith("0") || visorNumero.StartsWith("-0"))                            //A formatação de caracteres não exibe o número zero caso o número zero seja o único número na casa dos números inteiros, então nesse caso o número é exibido sem formatação 
                                {
                                    txtVisor.Text = visorNumero;
                                }
                                else
                                {
                                    txtVisor.Text = Convert.ToDecimal(visorNumero).ToString("###,###.##############");            //A formatação de caracteres é introduzida normalmente quando o número não começa com zero
                                }

                                txtVisorCalculo.Text = visorNumero + "+";                                      
                        }
                        else if (parteDecimal != "" && parteDecimal.TrimEnd('0') == "")                                          //Caso tenha-se colocado apenas números zero na casa decimal (valor nulo), ao se apertar um botão de operação os números zero e a vírgula serão removidos                                                
                        {                               
                            visorNumero = parteInteira.Remove(parteInteira.Length - 1);

                             if (visorNumero.StartsWith("0") || visorNumero.StartsWith("-0"))
                             {
                                 txtVisor.Text = visorNumero;
                             }
                             else
                             {
                                 txtVisor.Text = Convert.ToDecimal(visorNumero).ToString("###,###.##############");
                             }

                             txtVisorCalculo.Text = visorNumero + "+";
                             
                             parteInteira = "";
                             parteDecimal = "";
                        }
                        else if (parteInteira != "" && parteDecimal.TrimEnd('0') != "")                                       //Procedimento caso a casa decimal possua valor válido. As partes inteira e decimal de um número são reunidas em uma variável
                        {               
                           visorNumero = parteInteira + parteDecimal.TrimEnd('0');                                           //Caso a casa decimal seja um valor válido, mas possua números zero supérfluos, esses números zero serão removidos

                            if (visorNumero.StartsWith("0") || visorNumero.StartsWith("-0"))
                            {
                               txtVisor.Text = visorNumero;
                            }
                            else
                            {
                               txtVisor.Text = Convert.ToDecimal(visorNumero).ToString("###,###.##############");
                            }

                            txtVisorCalculo.Text = visorNumero + "+";
                            
                            parteInteira = "";
                            parteDecimal = "";
                        }
                        else                           //Caso o número com vírgula seja resultado das operações 1/x, raiz quadrada, elevar ao quadrado ou porcentagem
                        {
                            if (visorNumero.ToString().Contains('-') == false && visorNumero.ToString().Length > 17 || visorNumero.ToString().Contains('-') == true && visorNumero.ToString().Length > 18)
                            {
                                txtVisorCalculo.Text = Convert.ToDecimal(visorNumero).ToString("e") + "+";
                            }
                            else
                            {
                                txtVisorCalculo.Text += "+";
                            }
                        }
                    } 
                    else                                                                            //Procedimento caso o número não tenha vírgula
                    {
                        txtVisorCalculo.Text += "+";
                    }

                    primeiroNumero = Convert.ToDecimal(visorNumero);
                }
            }
            else if (segundoNumero == true  && calculoInvalido == false)                           //Procedimentos caso se aperte um botão de operação após informar o segundo número de um cálculo                                                    
            {
                if (visorResultado2 == true)
                {
                    visorResultado2 = false;
                }

                decimal resultado;

                if (visorNumero == "" && txtVisor.Text == "0")                                    //Procedimento caso o segundo número for o número zero introduzido pelo backspace
                {
                    visorNumero = "0";
                }
                else if (visorNumero.Contains(','))                                              
                {
                     if (visorNumero.EndsWith(",") == true && parteDecimal == "")
                     {
                           visorNumero = visorNumero.Remove(visorNumero.Length - 1);
                     }
                     else if (parteDecimal != "" && parteDecimal.TrimEnd('0') == "")
                     {
                           visorNumero = parteInteira.Remove(parteInteira.Length - 1);

                           parteInteira = "";
                           parteDecimal = "";
                     }
                     else if (parteInteira != "" && parteDecimal.TrimEnd('0') != "")
                     {
                           visorNumero = parteInteira + parteDecimal.TrimEnd('0');

                            parteInteira = "";
                            parteDecimal = "";
                     }
                 }

                 switch (operacao)                                                          //Caso se aperte um botão de operação depois de informar o segundo número de um cálculo, o resultado desse cálculo é tomado como o primeiro número de um próximo cálculo
                 {                                                                         //É levado em consideração que um cálculo pode gerar números muitos grandes ou pequenos que não sejam comportados pelo tipo de dado decimal
                     case "+":

                        try
                        {
                            resultado = primeiroNumero + Convert.ToDecimal(visorNumero);

                            if (resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 16 || resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 18)       //Todo resultado que gere números com mais de 16 dígitos é exibido em notação científica
                            {
                                txtVisor.Text = resultado.ToString("e");                  
                                txtVisorCalculo.Text = resultado.ToString("e") + "+";
                            }
                            else if (resultado < 1 && resultado > -1)                    //Resultados que comecem com o número zero sao exibidos sem formatação      
                            {
                                txtVisor.Text = resultado.ToString();
                                txtVisorCalculo.Text = resultado.ToString() + "+";
                            }
                            else
                            {
                                txtVisor.Text = resultado.ToString("###,###.###############");
                                txtVisorCalculo.Text = resultado.ToString() + "+";
                            }

                            visorNumero = resultado.ToString();
                        }
                        catch
                        {
                            txtVisorCalculo.Text = "Erro";
                            txtVisor.Text = "Estouro do limite";

                            calculoInvalido = true;
                        }

                        break;

                     case "-":

                        try
                        {
                            resultado = primeiroNumero - Convert.ToDecimal(visorNumero);

                            if (resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 16 || resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 18)
                            {
                                txtVisor.Text = resultado.ToString("e");
                                txtVisorCalculo.Text = resultado.ToString("e") + "+";
                            }
                            else if (resultado < 1 && resultado > -1)
                            {
                                txtVisor.Text = resultado.ToString();
                                txtVisorCalculo.Text = resultado.ToString() + "+";
                            }
                            else
                            {
                                txtVisor.Text = resultado.ToString("###,###.###############");
                                txtVisorCalculo.Text = resultado.ToString() + "+";
                            }

                            visorNumero = resultado.ToString();
                        }
                        catch
                        {
                            txtVisorCalculo.Text = "Erro";
                            txtVisor.Text = "Estouro do limite";

                            calculoInvalido = true;
                        }

                            break;

                     case "x":

                        try
                        {
                            resultado = primeiroNumero * Convert.ToDecimal(visorNumero);

                            if (resultado.ToString().Contains(',') == true && resultado.ToString().EndsWith("0") == true)       //Algumas vezes operações de multiplicação que envolvam números com casa decimal geram resultados com números zeros supérfulos na casa decimal
                            {
                                resultado = Convert.ToDecimal(resultado.ToString().TrimEnd('0'));
                            }

                            if (resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 16 || resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 18)
                            {
                                txtVisor.Text = resultado.ToString("e");
                                txtVisorCalculo.Text = resultado.ToString("e") + "+";
                            }
                            else if (resultado < 1 && resultado > -1)
                            {
                                txtVisor.Text = resultado.ToString();
                                txtVisorCalculo.Text = resultado.ToString() + "+";
                            }
                            else
                            {
                                txtVisor.Text = resultado.ToString("###,###.##############");
                                txtVisorCalculo.Text = resultado.ToString() + "+";
                            }

                            visorNumero = resultado.ToString();
                        }
                        catch
                        {
                            txtVisorCalculo.Text = "Erro";
                            txtVisor.Text = "Estouro do limite";
                        }

                        break;

                     case "/":

                        try
                        {
                            if (visorNumero == "0")
                            {
                                txtVisor.Text = "Cálculo inválido";
                                txtVisorCalculo.Text += "=";

                                calculoInvalido = true;
                            }
                            else
                            {
                                resultado = primeiroNumero / Convert.ToDecimal(visorNumero);

                                if (resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 16 || resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 18)
                                {
                                    txtVisor.Text = resultado.ToString("e");
                                    txtVisorCalculo.Text = resultado.ToString("e") + "+";
                                }
                                else if (resultado < 1 && resultado > -1)
                                {
                                    txtVisor.Text = resultado.ToString();
                                    txtVisorCalculo.Text = resultado.ToString() + "+";
                                }
                                else
                                {
                                    txtVisor.Text = resultado.ToString("###,###.##############");
                                    txtVisorCalculo.Text = resultado.ToString() + "+";
                                }

                                visorNumero = resultado.ToString();
                            }
                        }
                        catch
                        {
                            txtVisorCalculo.Text = "Erro";
                            txtVisor.Text = "Estouro do limite";

                            calculoInvalido = true;
                        }

                        break;
                 }
                    
                 novoNumero = true;
                 segundoNumero = false;
                    
                 if (calculoInvalido == true)
                 {
                    operacao = "";
                 }
                 else
                 {
                    operacao = "+";
                 }

                 primeiroNumero = Convert.ToDecimal(visorNumero);
            }
        }

        private void btnSubtracao_Click(object sender, EventArgs e)
        {
            if (segundoNumero == false && calculoInvalido == false)                                             
            {
                if (novoNumero == false)
                {
                    novoNumero = true;
                }

                if (visorResultado2 == true)
                {
                    visorResultado2 = false;
                }

                if (operacao != "-")
                {
                    operacao = "-";
                }


                if (txtVisorCalculo.Text.EndsWith("+") || txtVisorCalculo.Text.EndsWith("-") || txtVisorCalculo.Text.EndsWith("x") || txtVisorCalculo.Text.EndsWith("/"))    
                {
                    txtVisorCalculo.Text = txtVisorCalculo.Text.Remove(txtVisorCalculo.Text.Length - 1);

                    txtVisorCalculo.Text += "-";
                }
                else if (visorNumero == "")                                    
                {
                    txtVisorCalculo.Text = "0-";
                    visorNumero = "0";
                    primeiroNumero = Convert.ToDecimal(visorNumero);      
                }
                else if (visorResultado == true)                        
                {
                    if (visorNumero.ToString().Contains(',') == false && visorNumero.ToString().Contains('-') == false && visorNumero.ToString().Length > 16 || visorNumero.ToString().Contains(',') == false && visorNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 17 || visorNumero.ToString().Contains(',') == true && visorNumero.ToString().Contains('-') == false && visorNumero.ToString().Length > 17 || visorNumero.ToString().Contains(',') == true && visorNumero.ToString().Contains('-') == true && visorNumero.ToString().Length > 18)
                    {
                        txtVisorCalculo.Text = Convert.ToDecimal(visorNumero).ToString("e") + "-";
                    }
                    else
                    {
                        txtVisorCalculo.Text = visorNumero + "-";
                    }

                    primeiroNumero = Convert.ToDecimal(visorNumero);
                    visorResultado = false;
                }
                else
                {
                    if (visorNumero.Contains(','))                                                                           
                    {
                        if (visorNumero.EndsWith(",") == true && parteDecimal == "")                                        
                        {
                            visorNumero = visorNumero.Remove(visorNumero.Length - 1);

                            if (visorNumero.StartsWith("0") || visorNumero.StartsWith("-0"))                            
                            {
                                txtVisor.Text = visorNumero;
                            }
                            else
                            {
                                txtVisor.Text = Convert.ToDecimal(visorNumero).ToString("###,###.##############");            
                            }

                            txtVisorCalculo.Text = visorNumero + "-";
                        }
                        else if (parteDecimal != "" && parteDecimal.TrimEnd('0') == "")                                                                                        
                        {
                            visorNumero = parteInteira.Remove(parteInteira.Length - 1);

                            if (visorNumero.StartsWith("0") || visorNumero.StartsWith("-0"))
                            {
                                txtVisor.Text = visorNumero;
                            }
                            else
                            {
                                txtVisor.Text = Convert.ToDecimal(visorNumero).ToString("###,###.##############");
                            }

                            txtVisorCalculo.Text = visorNumero + "-";

                            parteInteira = "";
                            parteDecimal = "";
                        }
                        else if (parteInteira != "" && parteDecimal.TrimEnd('0') != "")                                       
                        {
                            visorNumero = parteInteira + parteDecimal.TrimEnd('0');                                           

                            if (visorNumero.StartsWith("0") || visorNumero.StartsWith("-0"))
                            {
                                txtVisor.Text = visorNumero;
                            }
                            else
                            {
                                txtVisor.Text = Convert.ToDecimal(visorNumero).ToString("###,###.##############");
                            }

                            txtVisorCalculo.Text = visorNumero + "-";

                            parteInteira = "";
                            parteDecimal = "";
                        }
                        else
                        {
                            if (visorNumero.ToString().Contains('-') == false && visorNumero.ToString().Length > 17 || visorNumero.ToString().Contains('-') == true && visorNumero.ToString().Length > 18)
                            {
                                txtVisorCalculo.Text = Convert.ToDecimal(visorNumero).ToString("e") + "-";
                            }
                            else
                            {
                                txtVisorCalculo.Text += "-";
                            }
                        }
                    }
                    else                                                                            
                    {
                        txtVisorCalculo.Text += "-";
                    }

                    primeiroNumero = Convert.ToDecimal(visorNumero);
                }
            }
            else if (segundoNumero == true && calculoInvalido == false)                                                                              
            {
                if (visorResultado2 == true)
                {
                    visorResultado2 = false;
                }

                decimal resultado;

                if (visorNumero == "" && txtVisor.Text == "0")                                    
                {
                    visorNumero = "0";
                }
                else if (visorNumero.Contains(','))
                {
                    if (visorNumero.EndsWith(",") == true && parteDecimal == "")
                    {
                        visorNumero = visorNumero.Remove(visorNumero.Length - 1);
                    }
                    else if (parteDecimal != "" && parteDecimal.TrimEnd('0') == "")
                    {
                        visorNumero = parteInteira.Remove(parteInteira.Length - 1);

                        parteInteira = "";
                        parteDecimal = "";
                    }
                    else if (parteInteira != "" && parteDecimal.TrimEnd('0') != "")
                    {
                        visorNumero = parteInteira + parteDecimal.TrimEnd('0');

                        parteInteira = "";
                        parteDecimal = "";
                    }
                }

                switch (operacao)
                {
                    case "+":

                        try
                        {
                            resultado = primeiroNumero + Convert.ToDecimal(visorNumero);

                            if (resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 16 || resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 18)
                            {
                                txtVisor.Text = resultado.ToString("e");
                                txtVisorCalculo.Text = resultado.ToString("e") + "-";
                            }
                            else if (resultado < 1 && resultado > -1)
                            {
                                txtVisor.Text = resultado.ToString();
                                txtVisorCalculo.Text = resultado.ToString() + "-";
                            }
                            else
                            {
                                txtVisor.Text = resultado.ToString("###,###.###############");
                                txtVisorCalculo.Text = resultado.ToString() + "-";
                            }

                            visorNumero = resultado.ToString();
                        }
                        catch
                        {
                            txtVisorCalculo.Text = "Erro";
                            txtVisor.Text = "Estouro do limite";

                            calculoInvalido = true;
                        }

                        break;

                    case "-":

                        try
                        {
                            resultado = primeiroNumero - Convert.ToDecimal(visorNumero);

                            if (resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 16 || resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 18)
                            {
                                txtVisor.Text = resultado.ToString("e");
                                txtVisorCalculo.Text = resultado.ToString("e") + "-";
                            }
                            else if (resultado < 1 && resultado > -1)
                            {
                                txtVisor.Text = resultado.ToString();
                                txtVisorCalculo.Text = resultado.ToString() + "-";
                            }
                            else
                            {
                                txtVisor.Text = resultado.ToString("###,###.###############");
                                txtVisorCalculo.Text = resultado.ToString() + "-";
                            }

                            visorNumero = resultado.ToString();
                        }
                        catch
                        {
                            txtVisorCalculo.Text = "Erro";
                            txtVisor.Text = "Estouro do limite";

                            calculoInvalido = true;
                        }

                            break;

                    case "x":

                        try
                        {
                            resultado = primeiroNumero * Convert.ToDecimal(visorNumero);

                            if (resultado.ToString().Contains(',') == true && resultado.ToString().EndsWith("0") == true)
                            {
                                resultado = Convert.ToDecimal(resultado.ToString().TrimEnd('0'));
                            }

                            if (resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 16 || resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 18)
                            {
                                txtVisor.Text = resultado.ToString("e");
                                txtVisorCalculo.Text = resultado.ToString("e") + "-";
                            }
                            else if (resultado < 1 && resultado > -1)
                            {
                                txtVisor.Text = resultado.ToString();
                                txtVisorCalculo.Text = resultado.ToString() + "-";
                            }
                            else
                            {
                                txtVisor.Text = resultado.ToString("###,###.##############");
                                txtVisorCalculo.Text = resultado.ToString() + "-";
                            }

                            visorNumero = resultado.ToString();
                        }
                        catch
                        {
                            txtVisorCalculo.Text = "Erro";
                            txtVisor.Text = "Estouro do limite";
                        }

                        break;

                    case "/":

                        try
                        {
                            if (visorNumero == "0")
                            {
                                txtVisor.Text = "Cálculo inválido";
                                txtVisorCalculo.Text += "=";

                                calculoInvalido = true;
                                operacao = "";
                            }
                            else
                            {
                                resultado = primeiroNumero / Convert.ToDecimal(visorNumero);

                                if (resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 16 || resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 18)
                                {
                                    txtVisor.Text = resultado.ToString("e");
                                    txtVisorCalculo.Text = resultado.ToString("e") + "-";
                                }
                                else if (resultado < 1 && resultado > -1)
                                {
                                    txtVisor.Text = resultado.ToString();
                                    txtVisorCalculo.Text = resultado.ToString() + "-";
                                }
                                else
                                {
                                    txtVisor.Text = resultado.ToString("###,###.##############");
                                    txtVisorCalculo.Text = resultado.ToString() + "-";
                                }

                                visorNumero = resultado.ToString();
                            }
                        }
                        catch
                        {
                            txtVisorCalculo.Text = "Erro";
                            txtVisor.Text = "Estouro do limite";

                            calculoInvalido = true;
                        }
                        
                        break;
                }
                
                novoNumero = true;
                segundoNumero = false;

                if (calculoInvalido == true)
                {
                    operacao = "";
                }
                else
                {
                    operacao = "-";
                }

                primeiroNumero = Convert.ToDecimal(visorNumero);
            }
        }

        private void btnMultiplicacao_Click(object sender, EventArgs e)
        {
            if (segundoNumero == false && calculoInvalido == false)
            {
                if (novoNumero == false)
                {
                    novoNumero = true;
                }

                if (visorResultado2 == true)
                {
                    visorResultado2 = false;
                }

                if (operacao != "x")
                {
                    operacao = "x";
                }


                if (txtVisorCalculo.Text.EndsWith("+") || txtVisorCalculo.Text.EndsWith("-") || txtVisorCalculo.Text.EndsWith("x") || txtVisorCalculo.Text.EndsWith("/"))
                {
                    txtVisorCalculo.Text = txtVisorCalculo.Text.Remove(txtVisorCalculo.Text.Length - 1);

                    txtVisorCalculo.Text += "x";
                }
                else if (visorNumero == "")
                {
                    txtVisorCalculo.Text = "0-";
                    visorNumero = "0";
                    primeiroNumero = Convert.ToDecimal(visorNumero);
                }
                else if (visorResultado == true)
                {
                    if (visorNumero.ToString().Contains(',') == false && visorNumero.ToString().Contains('-') == false && visorNumero.ToString().Length > 16 || visorNumero.ToString().Contains(',') == false && visorNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 17 || visorNumero.ToString().Contains(',') == true && visorNumero.ToString().Contains('-') == false && visorNumero.ToString().Length > 17 || visorNumero.ToString().Contains(',') == true && visorNumero.ToString().Contains('-') == true && visorNumero.ToString().Length > 18)
                    {
                        txtVisorCalculo.Text = Convert.ToDecimal(visorNumero).ToString("e") + "x";
                    }
                    else
                    {
                        txtVisorCalculo.Text = visorNumero + "x";
                    }

                    primeiroNumero = Convert.ToDecimal(visorNumero);
                    visorResultado = false;
                }
                else
                {
                    if (visorNumero.Contains(','))
                    {
                        if (visorNumero.EndsWith(",") == true && parteDecimal == "")
                        {
                            visorNumero = visorNumero.Remove(visorNumero.Length - 1);

                            if (visorNumero.StartsWith("0") || visorNumero.StartsWith("-0"))
                            {
                                txtVisor.Text = visorNumero;
                            }
                            else
                            {
                                txtVisor.Text = Convert.ToDecimal(visorNumero).ToString("###,###.##############");
                            }

                            txtVisorCalculo.Text = visorNumero + "-";
                        }
                        else if (parteDecimal != "" && parteDecimal.TrimEnd('0') == "")
                        {
                            visorNumero = parteInteira.Remove(parteInteira.Length - 1);

                            if (visorNumero.StartsWith("0") || visorNumero.StartsWith("-0"))
                            {
                                txtVisor.Text = visorNumero;
                            }
                            else
                            {
                                txtVisor.Text = Convert.ToDecimal(visorNumero).ToString("###,###.##############");
                            }

                            txtVisorCalculo.Text = visorNumero + "x";

                            parteInteira = "";
                            parteDecimal = "";
                        }
                        else if (parteInteira != "" && parteDecimal.TrimEnd('0') != "")
                        {
                            visorNumero = parteInteira + parteDecimal.TrimEnd('0');

                            if (visorNumero.StartsWith("0") || visorNumero.StartsWith("-0"))
                            {
                                txtVisor.Text = visorNumero;
                            }
                            else
                            {
                                txtVisor.Text = Convert.ToDecimal(visorNumero).ToString("###,###.##############");
                            }

                            txtVisorCalculo.Text = visorNumero + "x";

                            parteInteira = "";
                            parteDecimal = "";
                        }
                        else
                        {
                            if (visorNumero.ToString().Contains('-') == false && visorNumero.ToString().Length > 17 || visorNumero.ToString().Contains('-') == true && visorNumero.ToString().Length > 18)
                            {
                                txtVisorCalculo.Text = Convert.ToDecimal(visorNumero).ToString("e") + "x";
                            }
                            else
                            {
                                txtVisorCalculo.Text += "x";
                            }
                        }
                    }
                    else
                    {
                        txtVisorCalculo.Text += "x";
                    }

                    primeiroNumero = Convert.ToDecimal(visorNumero);
                }
            }
            else if (segundoNumero == true && calculoInvalido == false)
            {
                if (visorResultado2 == true)
                {
                    visorResultado2 = false;
                }

                decimal resultado;

                if (visorNumero == "" && txtVisor.Text == "0")
                {
                    visorNumero = "0";
                }
                else if (visorNumero.Contains(','))
                {
                    if (visorNumero.EndsWith(",") == true && parteDecimal == "")
                    {
                        visorNumero = visorNumero.Remove(visorNumero.Length - 1);
                    }
                    else if (parteDecimal != "" && parteDecimal.TrimEnd('0') == "")
                    {
                        visorNumero = parteInteira.Remove(parteInteira.Length - 1);

                        parteInteira = "";
                        parteDecimal = "";
                    }
                    else if (parteInteira != "" && parteDecimal.TrimEnd('0') != "")
                    {
                        visorNumero = parteInteira + parteDecimal.TrimEnd('0');

                        parteInteira = "";
                        parteDecimal = "";
                    }
                }

                switch (operacao)
                {
                    case "+":

                        try
                        {
                            resultado = primeiroNumero + Convert.ToDecimal(visorNumero);

                            if (resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 16 || resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 18)
                            {
                                txtVisor.Text = resultado.ToString("e");
                                txtVisorCalculo.Text = resultado.ToString("e") + "x";
                            }
                            else if (resultado < 1 && resultado > -1)
                            {
                                txtVisor.Text = resultado.ToString();
                                txtVisorCalculo.Text = resultado.ToString() + "x";
                            }
                            else
                            {
                                txtVisor.Text = resultado.ToString("###,###.###############");
                                txtVisorCalculo.Text = resultado.ToString() + "x";
                            }

                            visorNumero = resultado.ToString();
                        }
                        catch
                        {
                            txtVisorCalculo.Text = "Erro";
                            txtVisor.Text = "Estouro do limite";

                            calculoInvalido = true;
                        }

                        break;

                    case "-":

                        try
                        {
                            resultado = primeiroNumero - Convert.ToDecimal(visorNumero);

                            if (resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 16 || resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 18)
                            {
                                txtVisor.Text = resultado.ToString("e");
                                txtVisorCalculo.Text = resultado.ToString("e") + "x";
                            }
                            else if (resultado < 1 && resultado > -1)
                            {
                                txtVisor.Text = resultado.ToString();
                                txtVisorCalculo.Text = resultado.ToString() + "x";
                            }
                            else
                            {
                                txtVisor.Text = resultado.ToString("###,###.###############");
                                txtVisorCalculo.Text = resultado.ToString() + "x";
                            }

                            visorNumero = resultado.ToString();
                        }
                        catch
                        {
                            txtVisorCalculo.Text = "Erro";
                            txtVisor.Text = "Estouro do limite";

                            calculoInvalido = true;
                        }
                        
                        break;

                    case "x":

                        try
                        {
                            resultado = primeiroNumero * Convert.ToDecimal(visorNumero);

                            if (resultado.ToString().Contains(',') == true && resultado.ToString().EndsWith("0") == true)
                            {
                                resultado = Convert.ToDecimal(resultado.ToString().TrimEnd('0'));
                            }

                            if (resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 16 || resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 18)
                            {
                                txtVisor.Text = resultado.ToString("e");
                                txtVisorCalculo.Text = resultado.ToString("e") + "x";
                            }
                            else if (resultado < 1 && resultado > -1)
                            {
                                txtVisor.Text = resultado.ToString();
                                txtVisorCalculo.Text = resultado.ToString() + "x";
                            }
                            else
                            {
                                txtVisor.Text = resultado.ToString("###,###.##############");
                                txtVisorCalculo.Text = resultado.ToString() + "x";
                            }

                            visorNumero = resultado.ToString();
                        }
                        catch
                        {
                            txtVisorCalculo.Text = "Erro";
                            txtVisor.Text = "Estouro do limite";
                        }

                        break;

                    case "/":

                        try
                        {
                            if (visorNumero == "0")
                            {
                                txtVisor.Text = "Cálculo inválido";
                                txtVisorCalculo.Text += "=";

                                calculoInvalido = true;
                                operacao = "";
                            }
                            else
                            {
                                resultado = primeiroNumero / Convert.ToDecimal(visorNumero);

                                if (resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 16 || resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 18)
                                {
                                    txtVisor.Text = resultado.ToString("e");
                                    txtVisorCalculo.Text = resultado.ToString("e") + "x";
                                }
                                else if (resultado < 1 && resultado > -1)
                                {
                                    txtVisor.Text = resultado.ToString();
                                    txtVisorCalculo.Text = resultado.ToString() + "x";
                                }
                                else
                                {
                                    txtVisor.Text = resultado.ToString("###,###.##############");
                                    txtVisorCalculo.Text = resultado.ToString() + "x";
                                }

                                visorNumero = resultado.ToString();
                            }
                        }
                        catch
                        {
                            txtVisorCalculo.Text = "Erro";
                            txtVisor.Text = "Estouro do limite";

                            calculoInvalido = true;
                        }
                        
                        break;
                }
                
                novoNumero = true;
                segundoNumero = false;

                if (calculoInvalido == true)
                {
                    operacao = "";
                }
                else
                {
                    operacao = "x";
                }

                primeiroNumero = Convert.ToDecimal(visorNumero);
            }
        }

        private void btnDivisao_Click(object sender, EventArgs e)
        {
            if (segundoNumero == false && calculoInvalido == false)
            {
                if (novoNumero == false)
                {
                    novoNumero = true;
                }

                if (visorResultado2 == true)
                {
                    visorResultado2 = false;
                }

                if (operacao != "/")
                {
                    operacao = "/";
                }


                if (txtVisorCalculo.Text.EndsWith("+") || txtVisorCalculo.Text.EndsWith("-") || txtVisorCalculo.Text.EndsWith("x") || txtVisorCalculo.Text.EndsWith("/"))
                {
                    txtVisorCalculo.Text = txtVisorCalculo.Text.Remove(txtVisorCalculo.Text.Length - 1);

                    txtVisorCalculo.Text += "/";
                }
                else if (visorNumero == "")
                {
                    txtVisorCalculo.Text = "0/";
                    visorNumero = "0";
                    primeiroNumero = Convert.ToDecimal(visorNumero);
                }
                else if (visorResultado == true)
                {
                    if (visorNumero.ToString().Contains(',') == false && visorNumero.ToString().Contains('-') == false && visorNumero.ToString().Length > 16 || visorNumero.ToString().Contains(',') == false && visorNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 17 || visorNumero.ToString().Contains(',') == true && visorNumero.ToString().Contains('-') == false && visorNumero.ToString().Length > 17 || visorNumero.ToString().Contains(',') == true && visorNumero.ToString().Contains('-') == true && visorNumero.ToString().Length > 18)
                    {
                        txtVisorCalculo.Text = Convert.ToDecimal(visorNumero).ToString("e") + "/";
                    }
                    else
                    {
                        txtVisorCalculo.Text = visorNumero + "/";
                    }

                    primeiroNumero = Convert.ToDecimal(visorNumero);
                    visorResultado = false;
                }
                else
                {
                    if (visorNumero.Contains(','))
                    {
                        if (visorNumero.EndsWith(",") == true && parteDecimal == "")
                        {
                            visorNumero = visorNumero.Remove(visorNumero.Length - 1);

                            if (visorNumero.StartsWith("0") || visorNumero.StartsWith("-0"))
                            {
                                txtVisor.Text = visorNumero;
                            }
                            else
                            {
                                txtVisor.Text = Convert.ToDecimal(visorNumero).ToString("###,###.##############");
                            }

                            txtVisorCalculo.Text = visorNumero + "/";
                        }
                        else if (parteDecimal != "" && parteDecimal.TrimEnd('0') == "")
                        {
                            visorNumero = parteInteira.Remove(parteInteira.Length - 1);

                            if (visorNumero.StartsWith("0") || visorNumero.StartsWith("-0"))
                            {
                                txtVisor.Text = visorNumero;
                            }
                            else
                            {
                                txtVisor.Text = Convert.ToDecimal(visorNumero).ToString("###,###.##############");
                            }

                            txtVisorCalculo.Text = visorNumero + "/";

                            parteInteira = "";
                            parteDecimal = "";
                        }
                        else if (parteInteira != "" && parteDecimal.TrimEnd('0') != "")
                        {
                            visorNumero = parteInteira + parteDecimal.TrimEnd('0');

                            if (visorNumero.StartsWith("0") || visorNumero.StartsWith("-0"))
                            {
                                txtVisor.Text = visorNumero;
                            }
                            else
                            {
                                txtVisor.Text = Convert.ToDecimal(visorNumero).ToString("###,###.##############");
                            }

                            txtVisorCalculo.Text = visorNumero + "/";

                            parteInteira = "";
                            parteDecimal = "";
                        }
                        else
                        {
                            if (visorNumero.ToString().Contains('-') == false && visorNumero.ToString().Length > 17 || visorNumero.ToString().Contains('-') == true && visorNumero.ToString().Length > 18)
                            {
                                txtVisorCalculo.Text = Convert.ToDecimal(visorNumero).ToString("e") + "/";
                            }
                            else
                            {
                                txtVisorCalculo.Text += "/";
                            }
                        }
                    }
                    else
                    {
                        txtVisorCalculo.Text += "/";
                    }

                    primeiroNumero = Convert.ToDecimal(visorNumero);
                }
            }
            else if (segundoNumero == true && calculoInvalido == false)
            {
                if (visorResultado2 == true)
                {
                    visorResultado2 = false;
                }

                decimal resultado;

                if (visorNumero == "" && txtVisor.Text == "0")
                {
                    visorNumero = "0";
                }
                else if (visorNumero.Contains(','))
                {
                    if (visorNumero.EndsWith(",") == true && parteDecimal == "")
                    {
                        visorNumero = visorNumero.Remove(visorNumero.Length - 1);
                    }
                    else if (parteDecimal != "" && parteDecimal.TrimEnd('0') == "")
                    {
                        visorNumero = parteInteira.Remove(parteInteira.Length - 1);

                        parteInteira = "";
                        parteDecimal = "";
                    }
                    else if (parteInteira != "" && parteDecimal.TrimEnd('0') != "")
                    {
                        visorNumero = parteInteira + parteDecimal.TrimEnd('0');

                        parteInteira = "";
                        parteDecimal = "";
                    }
                }

                switch (operacao)
                {
                    case "+":

                        try
                        {
                            resultado = primeiroNumero + Convert.ToDecimal(visorNumero);

                            if (resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 16 || resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 18)
                            {
                                txtVisor.Text = resultado.ToString("e");
                                txtVisorCalculo.Text = resultado.ToString("e") + "/";
                            }
                            else if (resultado < 1 && resultado > -1)
                            {
                                txtVisor.Text = resultado.ToString();
                                txtVisorCalculo.Text = resultado.ToString() + "/";
                            }
                            else
                            {
                                txtVisor.Text = resultado.ToString("###,###.###############");
                                txtVisorCalculo.Text = resultado.ToString() + "/";
                            }

                            visorNumero = resultado.ToString();
                        }
                        catch
                        {
                            txtVisorCalculo.Text = "Erro";
                            txtVisor.Text = "Estouro do limite";

                            calculoInvalido = true;
                        }

                        break;

                    case "-":

                        try
                        {
                            resultado = primeiroNumero - Convert.ToDecimal(visorNumero);

                            if (resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 16 || resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 18)
                            {
                                txtVisor.Text = resultado.ToString("e");
                                txtVisorCalculo.Text = resultado.ToString("e") + "/";
                            }
                            else if (resultado < 1 && resultado > -1)
                            {
                                txtVisor.Text = resultado.ToString();
                                txtVisorCalculo.Text = resultado.ToString() + "/";
                            }
                            else
                            {
                                txtVisor.Text = resultado.ToString("###,###.###############");
                                txtVisorCalculo.Text = resultado.ToString() + "/";
                            }

                            visorNumero = resultado.ToString();
                        }
                        catch
                        {
                            txtVisorCalculo.Text = "Erro";
                            txtVisor.Text = "Estouro do limite";

                            calculoInvalido = true;
                        }

                        break;

                    case "x":

                        try
                        {
                            resultado = primeiroNumero * Convert.ToDecimal(visorNumero);

                            if (resultado.ToString().Contains(',') == true && resultado.ToString().EndsWith("0") == true)
                            {
                                resultado = Convert.ToDecimal(resultado.ToString().TrimEnd('0'));
                            }

                            if (resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 16 || resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 18)
                            {
                                txtVisor.Text = resultado.ToString("e");
                                txtVisorCalculo.Text = resultado.ToString("e") + "/";
                            }
                            else if (resultado < 1 && resultado > -1)
                            {
                                txtVisor.Text = resultado.ToString();
                                txtVisorCalculo.Text = resultado.ToString() + "/";
                            }
                            else
                            {
                                txtVisor.Text = resultado.ToString("###,###.##############");
                                txtVisorCalculo.Text = resultado.ToString() + "/";
                            }

                            visorNumero = resultado.ToString();
                        }
                        catch
                        {
                            txtVisorCalculo.Text = "Erro";
                            txtVisor.Text = "Estouro do limite";
                        }

                        break;

                    case "/":

                        try
                        {
                            if (visorNumero == "0")
                            {
                                txtVisor.Text = "Cálculo inválido";
                                txtVisorCalculo.Text += "=";

                                calculoInvalido = true;
                                operacao = "";
                            }
                            else
                            {
                                resultado = primeiroNumero / Convert.ToDecimal(visorNumero);

                                if (resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 16 || resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 18)
                                {
                                    txtVisor.Text = resultado.ToString("e");
                                    txtVisorCalculo.Text = resultado.ToString("e") + "/";
                                }
                                else if (resultado < 1 && resultado > -1)
                                {
                                    txtVisor.Text = resultado.ToString();
                                    txtVisorCalculo.Text = resultado.ToString() + "/";
                                }
                                else
                                {
                                    txtVisor.Text = resultado.ToString("###,###.##############");
                                    txtVisorCalculo.Text = resultado.ToString() + "/";
                                }

                                visorNumero = resultado.ToString();
                            }
                        }
                        catch
                        {
                            txtVisorCalculo.Text = "Erro";
                            txtVisor.Text = "Estouro do limite";

                            calculoInvalido = true;
                        }

                        break;
                }
                
                novoNumero = true;
                segundoNumero = false;

                if (calculoInvalido == true)
                {
                    operacao = "";
                }
                else
                {
                    operacao = "/";
                }

                primeiroNumero = Convert.ToDecimal(visorNumero);
            }
        }

        private void btnRaizQuadrada_Click(object sender, EventArgs e)
        {
            if (novoNumero == true && visorNumero == "")             //Procedimento caso se aperte o botão sobre o número zero inicial ou o número zero alcançado pelo backspace
            {
                visorNumero = "0";

                txtVisorCalculo.Text += "0";
            }
            else if (novoNumero == false && calculoInvalido == false)      //O botão só funciona em duas situações: quando se estiver informando um dos números de um cálculo e quando o visor estiver exibindo o resultado de um cálculo
            {
                if (visorNumero.Contains(',') == true)                    //Procedimentos caso o número do visor tenha vírgula
                {
                    if (visorNumero.EndsWith(",") == true && parteDecimal == "")
                    {
                        visorNumero = visorNumero.Remove(visorNumero.Length - 1);
                    }
                    else if (parteDecimal != "" && parteDecimal.TrimEnd('0') == "")
                    {
                        visorNumero = parteInteira.Remove(parteInteira.Length - 1);

                        parteInteira = "";
                        parteDecimal = "";
                    }
                    else if (parteInteira != "" && parteDecimal.TrimEnd('0') != "")
                    {
                        visorNumero = parteInteira + parteDecimal.TrimEnd('0');

                        parteInteira = "";
                        parteDecimal = "";
                    }
                }

                if (segundoNumero == false)            //Procedimento caso se aperte o botão no primeiro número de um cálculo
                {
                    if (visorNumero.Contains('-') == true)     //Não é possível calcular raiz quadrada de número negativo
                    {
                        if (visorNumero.Contains(',') == false && visorNumero.Contains('-') == false && visorNumero.Length > 16 || visorNumero.Contains(',') == false && visorNumero.Contains('-') == true && visorNumero.Length > 17 || visorNumero.Contains(',') == true && visorNumero.Contains('-') == false && visorNumero.Length > 17 || visorNumero.Contains(',') == true && visorNumero.Contains('-') == true && visorNumero.Length > 18)
                        {
                            txtVisorCalculo.Text = '√' + Convert.ToDecimal(visorNumero).ToString("e") + '=';
                        }
                        else
                        {
                            txtVisorCalculo.Text = '√' + visorNumero + '=';
                        }
                        
                        txtVisor.Text = "Cálculo inválido";

                        calculoInvalido = true;
                        novoNumero = true;
                    }
                    else
                    {
                        double resultado = Math.Sqrt(Convert.ToDouble(visorNumero));


                        if (resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 16 || resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 18)
                        {
                            txtVisor.Text = resultado.ToString("e");
                            txtVisorCalculo.Text = resultado.ToString("e");
                        }
                        else if (resultado < 1 && resultado > -1)
                        {
                            txtVisor.Text = resultado.ToString();
                            txtVisorCalculo.Text = resultado.ToString();
                        }
                        else
                        {
                            txtVisor.Text = resultado.ToString("###,###.################");
                            txtVisorCalculo.Text = resultado.ToString();
                        }

                        visorNumero = resultado.ToString();

                        if (visorResultado2 == false)
                        {
                            visorResultado2 = true;
                        }
                    }
                }
                else         //Procedimento caso se aperte o botão sobre o segundo número de um cálculo
                {
                    if (visorNumero.Contains('-') == true)
                    {
                        if (visorNumero.Contains(',') == false && visorNumero.Contains('-') == false && visorNumero.Length > 16 || visorNumero.Contains(',') == false && visorNumero.Contains('-') == true && visorNumero.Length > 17 || visorNumero.Contains(',') == true && visorNumero.Contains('-') == false && visorNumero.Length > 17 || visorNumero.Contains(',') == true && visorNumero.Contains('-') == true && visorNumero.Length > 18)
                        {
                            if (primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 16 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 18)
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString("e") + operacao + '√' + Convert.ToDecimal(visorNumero).ToString("e") + '=';
                            }
                            else
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString() + operacao + '√' + Convert.ToDecimal(visorNumero).ToString("e") + '=';
                            }
                        }
                        else
                        {
                            if (primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 16 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 18)
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString("e") + operacao + '√' + Convert.ToDecimal(visorNumero).ToString() + '=';
                            }
                            else
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString() + operacao + '√' + Convert.ToDecimal(visorNumero).ToString() + '-';
                            }
                        }
                        
                        txtVisor.Text = "Cálculo inválido";

                        calculoInvalido = true;
                        novoNumero = true;
                    }
                    else
                    {
                        double resultado = Math.Sqrt(Convert.ToDouble(visorNumero));


                        if (resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 16 || resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 18)
                        {
                            txtVisor.Text = resultado.ToString("e");


                            if (primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 16 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 18)
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString("e") + operacao + resultado.ToString("e");
                            }
                            else
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString() + operacao + resultado.ToString("e");
                            }
                        }
                        else if (resultado < 1 && resultado > -1)
                        {
                            txtVisor.Text = resultado.ToString();


                            if (primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 16 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 18)
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString("e") + operacao + resultado.ToString();
                            }
                            else
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString() + operacao + resultado.ToString();
                            }
                        }
                        else
                        {
                            txtVisor.Text = resultado.ToString("###,###.###############");


                            if (primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 16 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 18)
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString("e") + operacao + resultado.ToString();
                            }
                            else
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString() + operacao + resultado.ToString();
                            }
                        }

                        visorNumero = resultado.ToString();

                        if (visorResultado2 == false)
                        {
                            visorResultado2 = true;
                        }
                    }
                }
            }
            else if (visorResultado == true)       //Procedimento caso se aperte o botão sobre o resultado de um cálculo
            {
                if (visorNumero.Contains('-') == true)
                {
                    if (visorNumero.Contains(',') == false && visorNumero.Contains('-') == false && visorNumero.Length > 16 || visorNumero.Contains(',') == false && visorNumero.Contains('-') == true && visorNumero.Length > 17 || visorNumero.Contains(',') == true && visorNumero.Contains('-') == false && visorNumero.Length > 17 || visorNumero.Contains(',') == true && visorNumero.Contains('-') == true && visorNumero.Length > 18)
                    {
                        txtVisorCalculo.Text = '√' + Convert.ToDecimal(visorNumero).ToString("e") + '=';
                    }
                    else
                    {
                        txtVisorCalculo.Text = '√' + visorNumero + '=';
                    }

                    txtVisor.Text = "Cálculo inválido";

                    calculoInvalido = true;
                    novoNumero = true;
                }
                else
                {
                    double resultado = Math.Sqrt(Convert.ToDouble(visorNumero));


                    if (resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 16 || resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 18)
                    {
                        txtVisor.Text = resultado.ToString("e");
                        txtVisorCalculo.Text = resultado.ToString("e");
                    }
                    else if (resultado < 1 && resultado > -1)
                    {
                        txtVisor.Text = resultado.ToString();
                        txtVisorCalculo.Text = resultado.ToString();
                    }
                    else
                    {
                        txtVisor.Text = resultado.ToString("###,###.###############");
                        txtVisorCalculo.Text = resultado.ToString();
                    }

                    visorNumero = resultado.ToString();
                }
            }
        }

        private void btnElevado_Click(object sender, EventArgs e)           // Elevar aao quadrado o número do visor
        {
            if (novoNumero == true && visorNumero == "")             //Procedimento caso se aperte o botão sobre o número zero inicial ou o número zero alcançado pelo backspace
            {
                visorNumero = "0";

                txtVisorCalculo.Text += "0";
            }
            else if (novoNumero == false && calculoInvalido == false)      //O botão só funciona em duas situações: quando se estiver informando um dos números de um cálculo e quando o visor estiver exibindo o resultado de um cálculo
            {
                if (visorNumero.Contains(',') == true)                          //Procedimentos caso o número do visor tenha vírgula
                {
                    if (visorNumero.EndsWith(",") == true && parteDecimal == "")
                    {
                        visorNumero = visorNumero.Remove(visorNumero.Length - 1);
                    }
                    else if (parteDecimal != "" && parteDecimal.TrimEnd('0') == "")
                    {
                        visorNumero = parteInteira.Remove(parteInteira.Length - 1);

                        parteInteira = "";
                        parteDecimal = "";
                    }
                    else if (parteInteira != "" && parteDecimal.TrimEnd('0') != "")
                    {
                        visorNumero = parteInteira + parteDecimal.TrimEnd('0');

                        parteInteira = "";
                        parteDecimal = "";
                    }
                }

                
                if (segundoNumero == false)            //Procedimento caso se aperte o botão no primeiro número de um cálculo
                {
                    try
                    {
                        decimal resultado = Convert.ToDecimal(visorNumero) * Convert.ToDecimal(visorNumero);

                        if (resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 16 || resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 18)
                        {
                            txtVisor.Text = resultado.ToString("e");
                            txtVisorCalculo.Text = resultado.ToString("e");
                        }
                        else if (resultado < 1 && resultado > -1)
                        {
                            txtVisor.Text = resultado.ToString();
                            txtVisorCalculo.Text = resultado.ToString();
                        }
                        else
                        {
                            txtVisor.Text = resultado.ToString("###,###.################");
                            txtVisorCalculo.Text = resultado.ToString();
                        }

                        visorNumero = resultado.ToString();

                        if (visorResultado2 == false)
                        {
                            visorResultado2 = true;
                        }
                    }
                    catch
                    {
                        txtVisorCalculo.Text = "Erro";
                        txtVisor.Text = "Estouro do limite";

                        calculoInvalido = true;
                    }  
                }
                else         //Procedimento caso se aperte o botão sobre o segundo número de um cálculo
                {
                    try
                    {
                        decimal resultado = Convert.ToDecimal(visorNumero) * Convert.ToDecimal(visorNumero);

                        if (resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 16 || resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 18)
                        {
                            txtVisor.Text = resultado.ToString("e");


                            if (primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 16 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 18)
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString("e") + operacao + resultado.ToString("e");
                            }
                            else
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString() + operacao + resultado.ToString("e");
                            }
                        }
                        else if (resultado < 1 && resultado > -1)
                        {
                            txtVisor.Text = resultado.ToString();


                            if (primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 16 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 18)
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString("e") + operacao + resultado.ToString();
                            }
                            else
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString() + operacao + resultado.ToString();
                            }
                        }
                        else
                        {
                            txtVisor.Text = resultado.ToString("###,###.###############");


                            if (primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 16 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 18)
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString("e") + operacao + resultado.ToString();
                            }
                            else
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString() + operacao + resultado.ToString();
                            }
                        }

                        visorNumero = resultado.ToString();

                        if (visorResultado2 == false)
                        {
                            visorResultado2 = true;
                        }
                    }
                    catch
                    {
                       txtVisorCalculo.Text = "Erro";
                       txtVisor.Text = "Estouro do limite";

                       calculoInvalido = true;
                    }
                }
            }
            else if (visorResultado == true)       //Procedimento caso se aperte o botão sobre o resultado de um cálculo
            {
                try
                {
                    decimal resultado = Convert.ToDecimal(visorNumero) * Convert.ToDecimal(visorNumero);

                    if (resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 16 || resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 18)
                    {
                        txtVisor.Text = resultado.ToString("e");
                        txtVisorCalculo.Text = resultado.ToString("e");
                    }
                    else if (resultado < 1 && resultado > -1)
                    {
                        txtVisor.Text = resultado.ToString();
                        txtVisorCalculo.Text = resultado.ToString();
                    }
                    else
                    {
                        txtVisor.Text = resultado.ToString("###,###.###############");
                        txtVisorCalculo.Text = resultado.ToString();
                    }

                    visorNumero = resultado.ToString();
                }
                catch
                {
                    txtVisorCalculo.Text = "Erro";
                    txtVisor.Text = "Estouro do limite";

                    calculoInvalido = true;
                }
            }
        }

        private void btnPorcentagem_Click(object sender, EventArgs e)
        {
            if (segundoNumero == true)
            {
                if (operacao == "x" || operacao == "/")
                {
                    decimal porcentagem = Convert.ToDecimal(visorNumero) / 100;

                    if (porcentagem.ToString().Contains(',') == false && porcentagem.ToString().Contains('-') == false && porcentagem.ToString().Length > 16 || porcentagem.ToString().Contains(',') == false && porcentagem.ToString().Contains('-') == true && porcentagem.ToString().Length > 17 || porcentagem.ToString().Contains(',') == true && porcentagem.ToString().Contains('-') == false && porcentagem.ToString().Length > 17 || porcentagem.ToString().Contains(',') == true && porcentagem.ToString().Contains('-') == true && porcentagem.ToString().Length > 18)
                    {
                        txtVisor.Text = porcentagem.ToString("e");


                        if (primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 16 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 18)
                        {
                            txtVisorCalculo.Text = primeiroNumero.ToString("e") + operacao + porcentagem.ToString("e");
                        }
                        else
                        {
                            txtVisorCalculo.Text = primeiroNumero.ToString() + operacao + porcentagem.ToString("e");
                        }
                    }
                    else if (porcentagem < 1 && porcentagem > -1)
                    {
                        txtVisor.Text = porcentagem.ToString();


                        if (primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 16 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 18)
                        {
                            txtVisorCalculo.Text = primeiroNumero.ToString("e") + operacao + porcentagem.ToString();
                        }
                        else
                        {
                            txtVisorCalculo.Text = primeiroNumero.ToString() + operacao + porcentagem.ToString();
                        }
                    }
                    else
                    {
                        txtVisor.Text = porcentagem.ToString("###,###.###############");


                        if (primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 16 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 18)
                        {
                            txtVisorCalculo.Text = primeiroNumero.ToString("e") + operacao + porcentagem.ToString();
                        }
                        else
                        {
                            txtVisorCalculo.Text = primeiroNumero.ToString() + operacao + porcentagem.ToString();
                        }
                    }

                    visorNumero = porcentagem.ToString();
                }
                else
                {
                    decimal porcentagem = primeiroNumero / 100;
                    porcentagem *= Convert.ToDecimal(visorNumero);

                    if (porcentagem.ToString().Contains(',') == true && porcentagem.ToString().EndsWith("0") == true)
                    {
                        porcentagem = Convert.ToDecimal(porcentagem.ToString().TrimEnd('0'));
                    }

                    if (porcentagem.ToString().Contains(',') == false && porcentagem.ToString().Contains('-') == false && porcentagem.ToString().Length > 16 || porcentagem.ToString().Contains(',') == false && porcentagem.ToString().Contains('-') == true && porcentagem.ToString().Length > 17 || porcentagem.ToString().Contains(',') == true && porcentagem.ToString().Contains('-') == false && porcentagem.ToString().Length > 17 || porcentagem.ToString().Contains(',') == true && porcentagem.ToString().Contains('-') == true && porcentagem.ToString().Length > 18)
                    {
                        txtVisor.Text = porcentagem.ToString("e");


                        if (primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 16 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 18)
                        {
                            txtVisorCalculo.Text = primeiroNumero.ToString("e") + operacao + porcentagem.ToString("e");
                        }
                        else
                        {
                            txtVisorCalculo.Text = primeiroNumero.ToString() + operacao + porcentagem.ToString("e");
                        }
                    }
                    else if (porcentagem < 1 && porcentagem > -1)
                    {
                        txtVisor.Text = porcentagem.ToString();


                        if (primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 16 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 18)
                        {
                            txtVisorCalculo.Text = primeiroNumero.ToString("e") + operacao + porcentagem.ToString();
                        }
                        else
                        {
                            txtVisorCalculo.Text = primeiroNumero.ToString() + operacao + porcentagem.ToString();
                        }
                    }
                    else
                    {
                        txtVisor.Text = porcentagem.ToString("###,###.###############");


                        if (primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 16 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 18)
                        {
                            txtVisorCalculo.Text = primeiroNumero.ToString("e") + operacao + porcentagem.ToString();
                        }
                        else
                        {
                            txtVisorCalculo.Text = primeiroNumero.ToString() + operacao + porcentagem.ToString();
                        }
                    }

                    visorNumero = porcentagem.ToString();
                }

                visorResultado2 = true;
            }
        }

        private void btnDividirUm_Click(object sender, EventArgs e)                           //Ao se apertar o botão o número 1 é dividido pelo número que está no visor
        {  
            if (visorNumero == "" || visorNumero == "0")             //Procedimentos caso o número do visor for o número zero
            {
                if (segundoNumero == false)
                {
                    txtVisor.Text = "Cálculo inválido";
                    txtVisorCalculo.Text = "1/0=";

                    calculoInvalido = true;
                    novoNumero = true;
                }
                else
                {
                    txtVisor.Text = "Cálculo inválido";

                    if (primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 16 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 18)
                    {
                        txtVisorCalculo.Text = primeiroNumero.ToString("e") + operacao + "(1/0)=";
                    }
                    else
                    {
                        txtVisorCalculo.Text = primeiroNumero.ToString() + operacao + "1/0=";
                    }

                    calculoInvalido = true;
                    novoNumero = true;
                    operacao = "";
                }
            }
            else if (novoNumero == false && calculoInvalido == false)     //O botão só funciona em duas situações: quando se estiver informando um dos números de um cálculo e quando o visor estiver exibindo o resultado de um cálculo
            {
                if (visorNumero.Contains(',') == true)                          //Procedimentos caso o número do visor tenha vírgula
                {
                    if (visorNumero.EndsWith(",") == true && parteDecimal == "")
                    {
                        visorNumero = visorNumero.Remove(visorNumero.Length - 1);
                    }
                    else if (parteDecimal != "" && parteDecimal.TrimEnd('0') == "")
                    {
                        visorNumero = parteInteira.Remove(parteInteira.Length - 1);

                        parteInteira = "";
                        parteDecimal = "";
                    }
                    else if (parteInteira != "" && parteDecimal.TrimEnd('0') != "")
                    {
                        visorNumero = parteInteira + parteDecimal.TrimEnd('0');

                        parteInteira = "";
                        parteDecimal = "";
                    }
                }


                if (segundoNumero == false)               //Procedimentos caso o botão seja apertado quando estiver o primeiro número de um cálculo no visor
                {
                    decimal resultado = 1 / Convert.ToDecimal(visorNumero);

                    
                    if (resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 16 || resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 18)
                    {
                        txtVisor.Text = resultado.ToString("e");
                        txtVisorCalculo.Text = resultado.ToString("e");
                    }
                    else if (resultado < 1 && resultado > -1)
                    {
                        txtVisor.Text = resultado.ToString();
                        txtVisorCalculo.Text = resultado.ToString();
                    }
                    else
                    {
                        txtVisor.Text = resultado.ToString("###,###.###############");
                        txtVisorCalculo.Text = resultado.ToString();
                    }

                    visorNumero = resultado.ToString();

                    if (visorResultado2 == false)
                    {
                        visorResultado2 = true;
                    }
                }
                else if (segundoNumero == true)                     //Procedimentos caso se aperte o botão quando estiver o segundo número de um cálculo no visor
                {
                    decimal resultado = 1 / Convert.ToDecimal(visorNumero);

                    
                    if (resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 16 || resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 18)
                    {
                        txtVisor.Text = resultado.ToString("e");

                        if (primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 16 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 18)
                        {
                            txtVisorCalculo.Text = primeiroNumero.ToString("e") + operacao + resultado.ToString("e");   
                        }
                        else
                        {
                            txtVisorCalculo.Text = primeiroNumero.ToString() + operacao + resultado.ToString("e");   
                        }
                    }
                    else if (resultado < 1 && resultado > -1)
                    {
                        txtVisor.Text = resultado.ToString();

                        if (resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 16 || resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 18)
                        {
                            txtVisorCalculo.Text = primeiroNumero.ToString("e") + operacao + resultado.ToString();   
                        }
                        else
                        {
                            txtVisorCalculo.Text = primeiroNumero.ToString() + operacao + resultado.ToString(); 
                        }
                    }
                    else
                    {
                        txtVisor.Text = resultado.ToString("###,###.###############");

                        if (primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 16 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 18)
                        {
                            txtVisorCalculo.Text = primeiroNumero.ToString("e") + operacao + resultado.ToString();
                        }
                        else
                        {
                            txtVisorCalculo.Text = primeiroNumero.ToString() + operacao + resultado.ToString();
                        }
                    }

                    visorNumero = resultado.ToString();

                    if (visorResultado2 == false)
                    {
                        visorResultado2 = true;
                    }
                }
            }
            else if (visorResultado == true)                  //Procedimentos caso se aperte o botão quando o visor estiver exibindo o resultado de um cálculo
            {
                decimal resultado = 1 / Convert.ToDecimal(visorNumero);


                if (resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 16 || resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 18)
                {
                    txtVisor.Text = resultado.ToString("e");
                }
                else if (resultado < 1 && resultado > -1)
                {
                    txtVisor.Text = resultado.ToString();
                }
                else
                {
                    txtVisor.Text = resultado.ToString("###,###.##############");
                }


                if (visorNumero.Contains(',') == false && primeiroNumero.ToString().Contains('-') == false && visorNumero.ToString().Length > 16 || visorNumero.ToString().Contains(',') == false && visorNumero.ToString().Contains('-') == true && visorNumero.ToString().Length > 17 || visorNumero.ToString().Contains(',') == true && visorNumero.ToString().Contains('-') == false && visorNumero.ToString().Length > 17 || visorNumero.ToString().Contains(',') == true && visorNumero.ToString().Contains('-') == true && visorNumero.ToString().Length > 18)
                {
                    txtVisorCalculo.Text = "1" + "/" + Convert.ToDecimal(visorNumero).ToString("e") + "=";
                }
                else
                {
                    txtVisorCalculo.Text = "1" + "/" + Convert.ToDecimal(visorNumero).ToString() + "=";
                }

                visorNumero = resultado.ToString();
            }
        }

        private void botaoIgual_Click(object sender, EventArgs e)
        {
            if (segundoNumero == true && calculoInvalido == false)                              //O botão de igual só fica funcional caso os dois números de uma operação forem informados
            {
                decimal resultado;

                
                if (visorNumero == "" && txtVisor.Text == "0")                                 //Procedimento caso so segundo número for o número zero introduzido pelo backspace
                {
                    visorNumero = "0";

                    txtVisorCalculo.Text += visorNumero + "=";
                }
                else if (visorNumero.Contains(','))                                         //Procedimentos caso o segundo número tenha vírgula
                {
                    if (visorNumero.EndsWith(",") == true && parteDecimal == "")
                    {
                        visorNumero = visorNumero.Remove(visorNumero.Length - 1);


                        if (primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Length > 16)
                        {
                            txtVisorCalculo.Text = primeiroNumero.ToString("e") + operacao + visorNumero + "=";
                        }
                        else
                        {
                            txtVisorCalculo.Text = primeiroNumero.ToString() + operacao + visorNumero + "=";
                        }    
                    }
                    else if (parteDecimal != "" && parteDecimal.TrimEnd('0') == "")
                    {
                        visorNumero = parteInteira.Remove(parteInteira.Length - 1);

                        
                        if (primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Length > 16)
                        {
                             txtVisorCalculo.Text = primeiroNumero.ToString("e") + operacao + visorNumero + "="; 
                        }
                        else
                        {
                            txtVisorCalculo.Text = primeiroNumero.ToString() + operacao + visorNumero + "=";       
                        }

                        parteInteira = "";
                        parteDecimal = "";
                    }
                    else if (parteInteira != "" && parteDecimal.TrimEnd('0') != "")
                    {
                        visorNumero = parteInteira + parteDecimal.TrimEnd('0');

                       
                        if (primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Length > 16)
                        {
                            txtVisorCalculo.Text = primeiroNumero.ToString("e") + operacao + visorNumero + "="; 
                        }
                        else
                        {  
                            txtVisorCalculo.Text = primeiroNumero.ToString() + operacao + visorNumero + "=";
                        }

                        parteInteira = "";
                        parteDecimal = "";
                    }
                    else
                    {
                        txtVisorCalculo.Text += "=";
                    }
                }
                else
                {
                    txtVisorCalculo.Text += "=";
                }

                switch (operacao) 
                {
                    case "+":

                        try
                        {
                            resultado = primeiroNumero + Convert.ToDecimal(visorNumero);


                            if (resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 16 || resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 18)
                            {
                                txtVisor.Text = resultado.ToString("e");
                            }
                            else if (resultado < 1 && resultado > -1)                                                            
                            {
                                txtVisor.Text = resultado.ToString();
                            }
                            else
                            {
                                txtVisor.Text = resultado.ToString("###,###.##############");                  
                            }

                            visorNumero = resultado.ToString();

                            visorResultado = true;
                        }
                        catch
                        {
                            txtVisorCalculo.Text = "Erro";
                            txtVisor.Text = "Estouro do limite";
                            
                            calculoInvalido = true;
                        }

                        break;

                    case "-":

                        try
                        {
                            resultado = primeiroNumero - Convert.ToDecimal(visorNumero);


                            if (resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 16 || resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 18)
                            {
                                txtVisor.Text = resultado.ToString("e");
                            }
                            else if (resultado < 1 && resultado > -1)
                            {
                                txtVisor.Text = resultado.ToString();
                            }
                            else
                            {
                                txtVisor.Text = resultado.ToString("###,###.##############");
                            }

                            visorNumero = resultado.ToString();

                            visorResultado = true;
                        }
                        catch
                        {
                            txtVisorCalculo.Text = "Erro";
                            txtVisor.Text = "Estouro do limite";
                            
                            calculoInvalido = true;
                        }
                        
                        break;

                    case "x":
 
                        try
                        {
                            resultado = primeiroNumero * Convert.ToDecimal(visorNumero);

                            if (resultado.ToString().Contains(',') == true && resultado.ToString().EndsWith("0") == true)
                            {
                                resultado = Convert.ToDecimal(resultado.ToString().TrimEnd('0'));
                            }


                            if (resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 16 || resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 18)
                            {
                                txtVisor.Text = resultado.ToString("e");
                            }
                            else if (resultado < 1 && resultado > -1)
                            {
                                txtVisor.Text = resultado.ToString();
                            }
                            else
                            {
                                txtVisor.Text = resultado.ToString("###,###.##############");
                            }

                            visorNumero = resultado.ToString();

                            visorResultado = true;
                        }
                        catch
                        {
                            txtVisorCalculo.Text = "Erro";
                            txtVisor.Text = "Estouro do limite";
                            
                            calculoInvalido = true;
                        }

                        break;

                    case "/":

                        try
                        {
                            if (visorNumero == "0")
                            {
                                txtVisor.Text = "Cálculo inválido";

                                calculoInvalido = true;
                            }
                            else
                            {
                                resultado = primeiroNumero / Convert.ToDecimal(visorNumero);


                                if (resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 16 || resultado.ToString().Contains(',') == false && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == false && resultado.ToString().Length > 17 || resultado.ToString().Contains(',') == true && resultado.ToString().Contains('-') == true && resultado.ToString().Length > 18)
                                {
                                    txtVisor.Text = resultado.ToString("e");
                                }
                                else if (resultado < 1 && resultado > -1)
                                {
                                    txtVisor.Text = resultado.ToString();
                                }
                                else
                                {
                                    txtVisor.Text = resultado.ToString("###,###.##############");
                                }

                                visorNumero = resultado.ToString();

                                visorResultado = true;
                            }
                        }
                        catch
                        {
                            txtVisorCalculo.Text = "Erro";
                            txtVisor.Text = "Estouro do limite";
                            
                            calculoInvalido = true;
                        }
                       
                        break;
                }
                
                operacao = "";                       //Após exibir um resultado, a variável da operação é esvaziada para assim indicar que um novo primeiro número de uma novo cálculo pode ser informado
                novoNumero = true;
                segundoNumero = false;

                if (visorResultado2 == true)
                {
                    visorResultado2 = false;
                }
            }
        }

        private void btnPositivoNegativo_Click(object sender, EventArgs e)               //Tornar o número positivo do visor em negativo ou tornar o número negativo do visor em positivo
        {
           if (calculoInvalido == false && novoNumero == false)
           {
                if (segundoNumero == false)                                             //Procedimentos quando se aperta o botão sobre o primeiro número de um cálculo
                {
                    if (visorNumero.EndsWith(",") == true && parteInteira == "")       //Procedimento de quando se apertar o botão com um número com vírgula, mas sem ter informado nenhum número na casa decimal
                    {
                        if (visorNumero.Contains('-') == false)
                        {
                            visorNumero = '-' + visorNumero;
                        }
                        else
                        {
                            visorNumero = visorNumero.Remove(0, 1);
                        }


                        if (visorNumero.StartsWith("0") || visorNumero.StartsWith("-0"))
                        {
                            txtVisor.Text = visorNumero;
                        }
                        else
                        {
                            txtVisor.Text = Convert.ToDecimal(visorNumero).ToString("###,###") + ',';
                        }

                        txtVisorCalculo.Text = visorNumero;
                    }
                    else if (visorNumero.Contains(',') == true && parteInteira != "")        //Procedimento de quando se aperta o botão em número com vírgula e casa decimal
                    {
                        if (parteInteira.Contains('-') == false)
                        {
                            parteInteira = '-' + parteInteira;
                            visorNumero = '-' + visorNumero;
                        }
                        else
                        {
                            parteInteira = parteInteira.Remove(0, 1);
                            visorNumero = visorNumero.Remove(0, 1);
                        }

                        
                        if (parteInteira.StartsWith("0") || parteInteira.StartsWith("-0"))
                        {
                            txtVisor.Text = parteInteira + parteDecimal;
                        }
                        else
                        {
                            txtVisor.Text = Convert.ToDecimal(parteInteira).ToString("###,###") + ',' + parteDecimal;
                        }

                        txtVisorCalculo.Text = parteInteira + parteDecimal;
                    }
                    else if (visorNumero.Contains(',') == true && visorNumero.EndsWith(",") == false)      //Procedimento de quando o número tiver vírgula e for introduzido por um botão de cálculo associado à variável visorResultado2
                    {
                        if (visorNumero.Contains('-') == false)
                        {
                            visorNumero = '-' + visorNumero;
                        }
                        else
                        {
                            visorNumero = visorNumero.Remove(0, 1);
                        }


                        if (visorNumero.Contains('-') == false && visorNumero.Length > 17 || visorNumero.Contains('-') == true && visorNumero.Length > 18)
                        {
                            txtVisor.Text = Convert.ToDecimal(visorNumero).ToString("e");
                            txtVisorCalculo.Text = Convert.ToDecimal(visorNumero).ToString("e");
                        }
                        else if (visorNumero.StartsWith("0") || visorNumero.StartsWith("-0"))
                        {
                            txtVisor.Text = visorNumero;
                            txtVisorCalculo.Text = visorNumero;
                        }
                        else
                        {
                            txtVisor.Text = Convert.ToDecimal(visorNumero).ToString("###,###.###############");
                            txtVisorCalculo.Text = visorNumero;
                        }
                    }
                    else                                           //Procedimento de quando se apertar o botão sobre um número sem vírgula, seja ele introduzido pelos botões de números, seja ele introduzido pelo resultado de uma operação de um botão associado à variável visorResultado2
                    {
                        if (visorNumero.Contains('-') == false)
                        {
                            visorNumero = '-' + visorNumero;
                        }
                        else
                        {
                            visorNumero = visorNumero.Remove(0, 1);
                        }

                        if (visorNumero.Contains('-') == false && visorNumero.Length > 16 || visorNumero.Contains('-') == true && visorNumero.Length > 17)
                        {
                            txtVisor.Text = Convert.ToDecimal(visorNumero).ToString("e");
                            txtVisorCalculo.Text = Convert.ToDecimal(visorNumero).ToString("e");
                        }
                        else if (visorNumero.StartsWith("0") || visorNumero.StartsWith("-0"))
                        {
                            txtVisor.Text = visorNumero;
                            txtVisorCalculo.Text = visorNumero;
                        }
                        else
                        {
                            txtVisor.Text = Convert.ToDecimal(visorNumero).ToString("###,###");
                            txtVisorCalculo.Text = visorNumero;
                        }
                    }
                }
                else if (segundoNumero == true)                                       //Procedimentos quando se apertar o botão sobre o segundo número de um cálculo
                {
                    if (visorNumero.EndsWith(",") == true && parteInteira == "")
                    { 
                        if (visorNumero.Contains('-') == false)
                        {
                            visorNumero = '-' + visorNumero;
                        }
                        else
                        {
                            visorNumero = visorNumero.Remove(0, 1);
                        }


                        if (visorNumero.StartsWith("0") || visorNumero.StartsWith("-0"))
                        {
                            txtVisor.Text = visorNumero;
                        }
                        else
                        {
                            txtVisor.Text = Convert.ToDecimal(visorNumero).ToString("###,###") + ',';
                        }


                        if (primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 18 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 16)
                        {
                            txtVisorCalculo.Text = primeiroNumero.ToString("e") + operacao + visorNumero;
                        }
                        else
                        {
                            txtVisorCalculo.Text = primeiroNumero.ToString() + operacao + visorNumero;
                        }
                    }
                    else if (visorNumero.Contains(',') == true && parteInteira != "")
                    {
                        if (parteInteira.Contains('-') == false)
                        {
                            parteInteira = '-' + parteInteira;
                            visorNumero = '-' + visorNumero;
                        }
                        else
                        {
                            parteInteira = parteInteira.Remove(0, 1);
                            visorNumero = visorNumero.Remove(0, 1);
                        }


                        if (parteInteira.StartsWith("0") || parteInteira.StartsWith("-0"))
                        {
                            txtVisor.Text = parteInteira + parteDecimal;
                        }
                        else
                        {
                            txtVisor.Text = Convert.ToDecimal(parteInteira).ToString("###,###") + ',' + parteDecimal;
                        }

                        if (primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 18 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 16)
                        {
                            if (parteInteira.Contains('-') == false)
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString("e") + operacao + parteInteira + parteDecimal;
                            }
                            else
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString("e") + operacao + '(' + parteInteira + parteDecimal + ')';
                            }
                        }
                        else
                        {
                            txtVisorCalculo.Text = primeiroNumero.ToString() + operacao + parteInteira + parteDecimal;   
                        }
                    }
                    else if (visorNumero.Contains(',') == true && visorNumero.EndsWith(",") == false)
                    {
                        if (visorNumero.Contains('-') == false)
                        {
                            visorNumero = '-' + visorNumero;
                        }
                        else
                        {
                            visorNumero = visorNumero.Remove(0, 1);
                        }

                        
                        if (visorNumero.Contains('-') == false && visorNumero.Length > 17 || visorNumero.Contains('-') == true && visorNumero.Length > 18)
                        {
                            txtVisor.Text = Convert.ToDecimal(visorNumero).ToString("e");

                           
                            if (primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 18 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 16)
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString("e") + operacao + Convert.ToDecimal(visorNumero).ToString("e");
                            }
                            else
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString() + operacao + Convert.ToDecimal(visorNumero).ToString("e");
                            }
                        }
                        else if (visorNumero.StartsWith("0") || visorNumero.StartsWith("-0"))
                        {
                            txtVisor.Text = visorNumero;

                            
                            if (primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 18 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 16)
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString("e") + operacao + visorNumero;
                            }
                            else
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString() + operacao + visorNumero;   
                            }
                        }
                        else
                        {
                            txtVisor.Text = Convert.ToDecimal(visorNumero).ToString("###,###.###############");

                            
                            if (primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 18 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 16)
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString("e") + operacao + visorNumero;
                            }
                            else
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString() + operacao + visorNumero;
                            }
                        }
                    }
                    else
                    {
                        if (visorNumero.Contains('-') == false)
                        {
                            visorNumero = '-' + visorNumero;
                        }
                        else
                        {
                            visorNumero = visorNumero.Remove(0, 1);
                        }

                        
                        if (visorNumero.Contains('-') == false && visorNumero.Length > 16 || visorNumero.Contains('-') == true && visorNumero.Length > 17)
                        {
                            txtVisor.Text = Convert.ToDecimal(visorNumero).ToString("e");

                            
                            if (primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 18 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 16)
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString("e") + operacao + Convert.ToDecimal(visorNumero).ToString("e");
                            }
                            else
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString() + operacao + Convert.ToDecimal(visorNumero).ToString("e");
                            }
                        }
                        else if (visorNumero.StartsWith("0") || visorNumero.StartsWith("-0"))
                        {
                            txtVisor.Text = Convert.ToDecimal(visorNumero).ToString();


                            if (primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 18 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 16)
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString("e") + operacao + Convert.ToDecimal(visorNumero).ToString();
                            }
                            else
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString() + operacao + Convert.ToDecimal(visorNumero).ToString();
                            }
                        }
                        else
                        {
                            txtVisor.Text = Convert.ToDecimal(visorNumero).ToString("###,###");


                            if (primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 18 || primeiroNumero.ToString().Contains(',') == true && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == true && primeiroNumero.ToString().Length > 17 || primeiroNumero.ToString().Contains(',') == false && primeiroNumero.ToString().Contains('-') == false && primeiroNumero.ToString().Length > 16)
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString("e") + operacao + Convert.ToDecimal(visorNumero).ToString();
                            }
                            else
                            {
                                txtVisorCalculo.Text = primeiroNumero.ToString() + operacao + Convert.ToDecimal(visorNumero).ToString();
                            }
                        }
                    }
                }
           }
           else if (visorResultado == true)                       //Procedimentos caso se aperte o botão sobre um número que seja o resultdo de um cálculo (botão =)
           {
                if (visorNumero.Contains('-') == false)
                {
                    visorNumero = '-' + visorNumero;
                }
                else
                {
                    visorNumero = visorNumero.Remove(0, 1);
                }

                if (visorNumero.Contains(',') == true && visorNumero.Contains('-') == true && visorNumero.Length > 18 || visorNumero.Contains(',') == true && visorNumero.Contains('-') == false && visorNumero.Length > 17 || visorNumero.Contains(',') == false && visorNumero.Contains('-') == true && visorNumero.Length > 17 || visorNumero.Contains(',') == false && visorNumero.Contains('-') == false && visorNumero.Length > 16)
                {
                    txtVisor.Text = Convert.ToDecimal(visorNumero).ToString("e");

                    txtVisorCalculo.Text = Convert.ToDecimal(visorNumero).ToString("e");
                }
                else if (visorNumero.StartsWith("0") || visorNumero.StartsWith("-0"))
                {
                    txtVisor.Text = visorNumero;

                    txtVisorCalculo.Text = visorNumero;
                }
                else
                {
                    txtVisor.Text = Convert.ToDecimal(visorNumero).ToString("###,###.###############");

                    txtVisorCalculo.Text = visorNumero;
                }
           }
        }

        private void btnBackspace_Click(object sender, EventArgs e)                                        //Botão backspace
        {
            if (visorNumero != "0" && visorNumero != "" && novoNumero == false && visorResultado2 == false)
            {
                if (parteDecimal != "")
                {
                    parteDecimal = parteDecimal.Remove(parteDecimal.Length -1);

                    
                    if (visorNumero.StartsWith("0") || visorNumero.StartsWith("-0"))
                    {
                        txtVisor.Text = parteInteira + parteDecimal;
                    }
                    else
                    {
                        txtVisor.Text = Convert.ToDecimal(parteInteira).ToString("###,###.###############") + ',' + parteDecimal;
                    }

                    if (parteDecimal == "")
                    {
                        parteInteira = "";
                    }
                }
                else
                {
                    visorNumero = visorNumero.Remove(visorNumero.Length - 1);

                    if ( visorNumero == "")       //Quando se apaga todos os números do visor este deve exibir o número zero
                    {
                        txtVisor.Text = "0";

                        novoNumero = true;
                    }
                    else if (visorNumero == "-")         //Quando se usa o backspace sobre um número negativo de apenas um dígito, deve-se apagar não só o número mas também o sinal de negativo do visor de cálculo
                    {
                        txtVisor.Text = "0";

                        novoNumero = true;
                        visorNumero = "";

                        txtVisorCalculo.Text = txtVisorCalculo.Text.Remove(txtVisorCalculo.Text.Length - 1);
                    }
                    else if (visorNumero == "-0")     //Caso se uso o backspace em um número negativo com casa decimal em que parte inteira seja o número zero, caso o usuário remova a vírgula será removido o sinal de negativo
                    {
                        novoNumero = true;
                        visorNumero = "0";

                        txtVisor.Text = visorNumero;

                        if (segundoNumero == false)
                        {
                            txtVisorCalculo.Text = txtVisorCalculo.Text.Remove(0, 1);
                        }
                        else
                        {
                            txtVisorCalculo.Text = txtVisorCalculo.Text.Remove(txtVisorCalculo.Text.LastIndexOf('-'), 1);
                        }
                    }
                    else if (visorNumero.StartsWith("0") || visorNumero.StartsWith("-0"))
                    {
                        txtVisor.Text = visorNumero;
                    }
                    else
                    {
                        txtVisor.Text = Convert.ToDecimal(visorNumero).ToString("###,###");
                    }
                }
               
                txtVisorCalculo.Text = txtVisorCalculo.Text.Remove(txtVisorCalculo.Text.Length - 1);
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)           //Botão que deixa a calculadora como no estado inicial
        {
            if (txtVisor.Text != "0")
            {
               txtVisor.Text = "0";
            }

            if (txtVisorCalculo.Text != "")
            {
               txtVisorCalculo.Clear();
            }

            if (operacao != "")
            {
               operacao = "";
            }

            if (visorNumero != "")
            {
               visorNumero = "";
            }

            if (parteDecimal != "")
            {
               parteInteira = "";
               parteDecimal = "";
            }
            
            if (novoNumero == false)
            {
               novoNumero = true;
            }
            
            if (segundoNumero == true)
            {
               segundoNumero = false;
            }

            if (visorResultado == true)
            {
                visorResultado = false;
            }

            if (visorResultado2 == true)
            {
                visorResultado = false;
            }

            if (calculoInvalido == true)
            {
               calculoInvalido = false;
            }
        }
    }
}
