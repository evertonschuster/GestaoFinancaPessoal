using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GestaoFinancaPessoal.Uteis.Initializer
{
    public class PesquisaCampoMetodo
    {
        public static void Main1(string[] args)
        {
            //Obtém o Type através do método GetType 
            Type tipo = Type.GetType(args[0]); //Consulta os campos 
            ConsultarCampos(tipo); //Consulta os métodos 
            ConsultarMetodos(tipo);
            Console.ReadLine();
        }

        //Método para consultar os campos do tipo 
        public static void ConsultarCampos(Type pTipo)
        { //Recupera os campos do tipo 
            FieldInfo[] campos = pTipo.GetFields();
            Listar("Campos do tipo", campos);
            //Campos sem modificador de acesso, ou seja, sem public, private etc 
            campos = pTipo.GetFields(BindingFlags.Default);
            Listar("Campos sem modificador de acesso", campos); //Consulta todos os campos públicos e de instância 
            campos = pTipo.GetFields(BindingFlags.Public | BindingFlags.Instance);
            Listar("Campos publicos e de instancia", campos); //Consulta todos os campos não públicos e de instância 
            campos = pTipo.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            Listar("Campos NAO publicos e de instancia", campos); //Consulta todos os campos públicos e estáticos 
            campos = pTipo.GetFields(BindingFlags.Static | BindingFlags.Public);
            Listar("Campos estaticos e publicos", campos); //Consulta todos os campos Não públicos e estáticos 
            campos = pTipo.GetFields(BindingFlags.Static | BindingFlags.NonPublic);
            Listar("Campos estaticos e NAO publicos", campos); //Consulta todos os campos declarados somente públicos 
            campos = pTipo.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Public);
            Listar("Campos somente declarados publicos", campos); //Consulta todos os campos 
            MemberInfo[] membros = pTipo.FindMembers(MemberTypes.Field, BindingFlags.Default, Type.FilterName, "Get*");
            Listar("Campos encontrados na pesquisa", membros);
        } //Método para consultar os métodos do tipo 

        public static void ConsultarMetodos(Type pTipo)
        { //Recupera os métodos do tipo 
            MethodInfo[] metodos = pTipo.GetMethods();
            Listar("Metodos do tipo", metodos); //Métodos sem modificar de acesso, ou seja, sem public, private etc 
            metodos = pTipo.GetMethods(BindingFlags.Default);
            Listar("Metodos sem modificador de acesso", metodos); //Consulta todos os métodos públicos e de instância 
            metodos = pTipo.GetMethods(BindingFlags.Public | BindingFlags.Instance);
            Listar("Metodos publicos e de instancia", metodos); //Consulta todos os métodos não públicos e de instância 
            metodos = pTipo.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);
            Listar("Metodos NAO públicos e de instancia", metodos); //Consulta todos os métodos públicos e estáticos
            metodos = pTipo.GetMethods(BindingFlags.Static | BindingFlags.Public);
            Listar("Metodos estaticos e publicos", metodos); //Consulta todos os métodos Não públicos e estáticos 
            metodos = pTipo.GetMethods(BindingFlags.Static | BindingFlags.NonPublic);
            Listar("Metodos estaticos e NAO publicos", metodos); //Consulta todos os métodos declarados somente públicos 
            metodos = pTipo.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Public);
            Listar("Metodos somente declarados publicos", metodos); //Consulta todos os métodos 
            MemberInfo[] membros = pTipo.FindMembers(MemberTypes.Method, BindingFlags.Default, Type.FilterName, "Get*");
            Listar("Metodos encontrados na pesquisa", membros);
        } //Método para listar os membros recuperados

        public static void Listar(String texto, MemberInfo[] membros)
        {
            Console.WriteLine("-------------");
            Console.WriteLine(texto);
            Console.WriteLine("-------------");
            foreach (MemberInfo membro in membros)
            {
                Console.WriteLine("Nome = " + membro.Name);
            }
        }
    }
}
