using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace BancoService
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IBancoServicio
    {
        [OperationContract]
        void Depositar(int cantidad,int id);
        [OperationContract]
        void Retirar(int Cantidad, int id);
        [OperationContract]
        List<Cliente> Extracto(int id);
        int Saldo_Actual(int id);
    }


    // Utilice un contrato de datos, como se ilustra en el ejemplo siguiente, para agregar tipos compuestos a las operaciones de servicio.
    [DataContract]
    public class Cliente
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int Saldo { get; set; }
        [DataMember]
        public DateTime Fecha_Operacion { get; set; }
        [DataMember]
        public Operaciones Operacion { get; set; }
    }
    [DataContract]
    public enum Operaciones
    {
        Deposito=1,
        Retiro=2
    }
}
