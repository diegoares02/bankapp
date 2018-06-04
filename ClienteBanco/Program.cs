using ClienteBanco.BancoServicio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteBanco
{
    class Program
    {
        static void Main(string[] args)
        {
            int op = 0, id, cantidad;
            BancoServicioClient cliente = new BancoServicioClient();
            Console.WriteLine("Ingrese su usuario");
            id = Convert.ToInt32(Console.ReadLine());
            do
            {
                Console.Clear();
                Console.WriteLine("1. Depositar");
                Console.WriteLine("2. Retirar");
                Console.WriteLine("3. Extracto");
                Console.WriteLine("4. Salir");
                Console.WriteLine("Ingrese una opcion");
                op = Convert.ToInt32(Console.ReadLine());
                switch (op)
                {
                    case 1:
                        Console.WriteLine("Ingrese la cantidad a depositar");
                        cantidad = Convert.ToInt32(Console.ReadLine());
                        cliente.Depositar(cantidad, id);
                        Console.WriteLine("Se realizo el deposito de " + cantidad + " con exito");
                        cliente.Close();
                        break;
                    case 2:
                        Console.WriteLine("Ingrese la cantidad a depositar");
                        cantidad = Convert.ToInt32(Console.ReadLine());
                        cliente.Retirar(cantidad, id);
                        Console.WriteLine("Se realizo el retiro de " + cantidad + " con exito");
                        cliente.Close();
                        break;
                    case 3:
                        Console.WriteLine("ID||Saldo||Fecha||Operacion");
                        try
                        {
                            foreach (var item in cliente.Extracto(id))
                            {
                                string cadena = item.Id + "||" + item.Saldo + "||" + item.Fecha_Operacion + "||" + item.Operacion;
                                Console.WriteLine(cadena);
                            }
                            cliente.Close();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }                        
                        break;
                }
                Console.ReadKey();
            } while (op != 4);
        }
    }
}
