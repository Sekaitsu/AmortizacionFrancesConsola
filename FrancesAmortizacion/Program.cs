using System;
using System.Data;
using DataTablePrettyPrinter;// Install-Package DataTablePrettyPrinter -Version 0.2.0

namespace FrancesAmortizacion
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                int     saldoPrestado = 0, periodo = 0;
                float   cuotaPagar = 0, cuotaPagarAval=0,
                        interes = 0, amortizacion = 0, 
                        saldoInicial = 0, saldoFinal = 0,
                        tasa = 0, porcTasa = 0,
                        aval = 0, porcAval =0 ;

                Console.WriteLine("Ingrese el valor del monto prestado: ");
                saldoPrestado = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Ingrese el interes del prestamo: ");
                tasa = (float)Convert.ToDouble(Console.ReadLine());
                porcTasa = tasa / 100;

                Console.WriteLine("Ingrese el numero de cotas para pagar: ");
                periodo = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("Ingrese el aval: ");
                aval = (float)Convert.ToDouble(Console.ReadLine());
                porcAval = (saldoPrestado * (aval / 100))/periodo;

                //(CREACION DE LA TABLA)//
                DataTable table = new DataTable();
                table.TableName = "Amortizacion";

                table.Columns.Add("Periodo", typeof(int));
                table.Columns.Add("Saldo Inicial", typeof(float));
                table.Columns.Add("Interés", typeof(float));
                table.Columns.Add("Abono a capital (Amortización)", typeof(float));
                table.Columns.Add("Cuota Mensual Fija - sin Aval", typeof(float));
                table.Columns.Add("Cuota Mensual Fija - con Aval", typeof(float));
                table.Columns.Add("Aval", typeof(float));
                table.Columns.Add("Saldo Final", typeof(float));

                saldoInicial = saldoPrestado;

                cuotaPagar = (float)(saldoPrestado * (Math.Pow(1 + porcTasa, periodo) * porcTasa) / (Math.Pow(1 + porcTasa, periodo) - 1));

                for (int i = 1; i <= periodo; i++)
                {
                    interes = saldoInicial * porcTasa;
                    amortizacion = (cuotaPagar - interes);
                    saldoFinal = saldoInicial - amortizacion;
                    cuotaPagarAval= (cuotaPagar + porcAval);

                    if (saldoFinal < 0)
                    {
                        saldoFinal = 0;
                    }
                    table.Rows.Add(i, saldoInicial, interes, amortizacion, cuotaPagar, cuotaPagarAval, porcAval, saldoFinal);

                    saldoInicial = saldoFinal;

                }
                Console.WriteLine(table.ToPrettyPrintedString());
            }
            catch
            {
                Console.WriteLine("ya la embarraste WEYYY :,c ");
            }
        }
    }
}