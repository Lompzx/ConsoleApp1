//Exercicio inserido no GitHub para Download e Teste -> 
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime vencOriginal, dataPgto;
            double valBoleto, totJuros, valBoletoRecalculado;
            const double taxa = 0.03;
            const int multa = 2;

            Console.WriteLine("Digite o Valor do Boleto");
            valBoleto = double.Parse(Console.ReadLine());
            Console.WriteLine("Digite a Data de Vencimento");
            vencOriginal = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Digite a Data Pagto");
            dataPgto = DateTime.Parse(Console.ReadLine());


            if (!VerificaFinalDeSemana(vencOriginal) && !VerificaFeriado(vencOriginal))
            {

                /*Se o vencimento for dia útil, e o pagamento no mesmo dia, não deve ser cobrado juros nem multa;
                Se a data de pagamento for anterior à data de vencimento, não deve ser cobrado juros nem multa;*/
                if (dataPgto.Date == vencOriginal || dataPgto.Date < vencOriginal.Date)
                {
                    Console.WriteLine("Não foi cobrado juros/multas, valor boleto: " + valBoleto);
                }
                else
                {
                    /*Se o vencimento for dia útil, e o pagamento no dia útil consecutivo, dever ser cobrado juros e multa de apenas um período. Ex.: Vencimento 20/julho/2017 pagamento 21/julho/2017: um dia de juros + multa.*/
                    /*Entra se a data de vencimento nao for final de semana nem feriado e as datas de pagamentos nao forem no mesmo dia ou dia anterios*/
                    var dias = vencOriginal - dataPgto;
                    totJuros = Math.Abs(dias.Days) * taxa;
                    valBoletoRecalculado = valBoleto + totJuros + multa;
                    Console.WriteLine("Vencimento: " + vencOriginal.ToString("d") + " Não é feriado nem final de semana!");
                    Console.WriteLine("O valor do Juro foi: " + totJuros.ToString("F") + " Multa de: " + multa + " Valor do Boleto Recalculado: " + valBoletoRecalculado);
                    Console.ReadLine();                    
                }
                
            }
            else if (VerificaFinalDeSemana(vencOriginal) || VerificaFeriado(vencOriginal))
            {
                /*Se a data de vencimento for feriado antecessor a um final de semana, e o pagamento for na segunda-feira 
                 * (dia útil consecutivo), não deve ser cobrado juros nem multa.
                 * Ex.: Vencimento 01/05/2020 Pago 04/05/2020 ou 25/12/2020 Pago 28/12/2020;*/
                if (vencOriginal.DayOfWeek == DayOfWeek.Saturday - 1)
                {   
                    if (dataPgto.DayOfWeek == DayOfWeek.Monday)
                    {
                        Console.WriteLine("Vencimento feriado que antecede um FDS,\b pagamento realizado na segunda-feira, não foi cobrado juros! \b Valor Boleto: " + valBoleto);
                    }
                    else
                    {
                        /*Se a data de vencimento for final de semana ou feriado, e o pagamento for posterior ao dia útil consecutivo, deve ser cobrado juros de todo o período. Ex.: Vencimento sábado e pagamento na terça-feira: três dias de juros + multa.*/
                        if (dataPgto.Day > vencOriginal.Day + 1)
                        {
                            var dias = vencOriginal - dataPgto;
                            totJuros = Math.Abs(dias.Days) * taxa;
                            valBoletoRecalculado = valBoleto + totJuros + multa;
                            Console.WriteLine("Vencimento: " + vencOriginal.ToString("d") + " É feriado ou final de semana, porém a data de pagamento foi posterior ao dia útil consecutivo!");
                            Console.WriteLine("O valor do Juro foi: " + totJuros.ToString("F") + " Multa de: " + multa + " Valor do Boleto Recalculado: " + valBoletoRecalculado);
                            Console.ReadLine();
                        }
                        else
                        {
                            /*Se a data de vencimento for feriado, e o pagamento no dia seguinte (dia útil), não deve ser cobrado juros nem multa. Ex.: Vencimento 15/junho/2017 e pagamento 16/junho/2017;*/
                            if (dataPgto.Day == vencOriginal.Day + 1)
                            {
                                valBoletoRecalculado = valBoleto;
                                Console.WriteLine("Vencimento: " + vencOriginal.ToString("d") + " Pagamento realizado no próximo dia útil.");
                                Console.WriteLine("Valor do Boleto : " + valBoletoRecalculado);
                                Console.ReadLine();
                            }
                        }
                    }
                }
            }            
            Console.ReadLine();

            bool VerificaFeriado(DateTime data)
            {
                if (data.ToString() == "01/01/2020 00:00:00" /*Ano Novo*/ || data.ToString() == "21/04/2020 00:00:00" /*Tiradentes*/
                    || data.ToString() == "01/05/2020 00:00:00" /*DiaDoTrabalho*/|| data.ToString() == "07/09/2020 00:00:00" /*Independencia*/
                     || data.ToString() == "12/10/2020 00:00:00" /*NossaSenhora*/ || data.ToString() == "02/11/2020 00:00:00"/*Finados*/
                     || data.ToString() == "15/11/2020 00:00:00"/*ProcRepublica*/ || data.ToString() == "25/12/2020 00:00:00" /*Natal*/)
                {
                    return true;
                }
                return false;
            }

            bool VerificaFinalDeSemana(DateTime data)
            {
                if (data.DayOfWeek == DayOfWeek.Saturday || data.DayOfWeek == DayOfWeek.Sunday)
                {
                    return true;
                }
                return false;
            }

        }
    }
}
