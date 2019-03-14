using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;


namespace GestaoFinancaPessoal.Uteis.Initializer
{
    public class ExlporaType
    {
        public static void Main1(string[] args)
        { //Obtém o objeto Type da classe System.Type. Poderia ser qualquer classe. 
            Type tipo = typeof(System.Type); //Verifica se é uma classe abstrata. Caso não, imprime todos os //seus construtores. Uma classe abstrata não pode ser instanciada. //Sendo assim, não possui construtores. 

            if (!tipo.IsAbstract)
            {
                Console.WriteLine("Type tem os seguintes construtores:");
                ImprimeConstrutores(tipo);
            } //Imprime todos os métodos 

            ImprimeMetodos(tipo); //Imprime todas as propriedades 
            ImprimePropriedades(tipo); //Imprime todos os campos 
            ImprimeCampos(tipo); //Imprime as interfaces que o objeto implementa 
            ImprimeInterfaces(tipo); //Imprime os atributos 
            ImprimeAtributos(tipo); Console.ReadLine();




        }

        //Método para imprimir todos os construtores da classe 
        private static void ImprimeConstrutores(Type pTipo)
        {
            Console.WriteLine(" $$$$$ Imprimindo os construtores da classe System.Type"); Console.WriteLine(" ");
            ConstructorInfo[] construtores = pTipo.GetConstructors();
            Console.WriteLine("Existe(m) " + construtores.Length + " construtores. ");
            Console.WriteLine(" ");

            foreach (ConstructorInfo construtor in construtores)
            {
                Console.WriteLine("Nome = " + construtor.Name);
                ParameterInfo[] parametros = construtor.GetParameters();
                if (parametros.Length > 0)
                    Console.WriteLine(" ----- Parâmetros do construtor ---- ");

                foreach (ParameterInfo parametro in parametros)
                {
                    Console.WriteLine("Posição = " + parametro.Position); Console.WriteLine("Nome = " + parametro.Name);
                    Console.WriteLine("Tipo = " + parametro.GetType()); Console.WriteLine(" ");
                }
                Console.WriteLine("======================="); Console.WriteLine(" ");
            }
        }

        //Método para imprimir todos os campos da classe. 
        private static void ImprimeCampos(Type pTipo)
        {
            Console.WriteLine(" $$$$$ Imprimindo os campos da classe System.type");
            Console.WriteLine(" ");
            FieldInfo[] campos = pTipo.GetFields();
            Console.WriteLine("Existe(m) " + campos.Length + " campos. ");
            Console.WriteLine(" ");
            foreach (FieldInfo campo in campos)
            {
                Console.WriteLine("Nome campo = " + campo.Name);
                Console.WriteLine("Tipo = " + campo.GetType());
                Console.WriteLine("É serializado = " + !campo.IsNotSerialized);
                Console.WriteLine("É privado = " + campo.IsPrivate); Console.WriteLine("É estático = " + campo.IsStatic);
                Console.WriteLine(" ");
            }
        }

        //Método para imprimir todas as propriedades da classe. 
        private static void ImprimePropriedades(Type pTipo)
        {
            Console.WriteLine(" $$$$$ Imprimindo as propriedades da classe System.type"); Console.WriteLine(" "); //Obtém as propriedades através do método GetProperties 
            PropertyInfo[] props = pTipo.GetProperties();
            Console.WriteLine("Existe(m) " + props.Length + "propriedades. ");
            Console.WriteLine(" ");
            foreach (PropertyInfo propriedade in props)
            {
                Console.WriteLine("Nome = " + propriedade.Name);
                Console.WriteLine("Tipo = " + propriedade.GetType());
                Console.WriteLine("Leitura = " + propriedade.CanRead);
                Console.WriteLine("Escrita = " + propriedade.CanWrite);
                Console.WriteLine(" ");
            }
        }

        //Método para imprimir todas as interfaces que o objeto implementa 
        private static void ImprimeInterfaces(Type pTipo)
        {
            Console.WriteLine(" $$$$$ Imprimindo as interfaces implementadas pela classe " + "System.Type");
            Console.WriteLine(" "); //Obtém as interfaces através do método GetInterfaces 
            Type[] interfaces = pTipo.GetInterfaces();
            Console.WriteLine("Existe(m) " + interfaces.Length + " interfaces implementadas. ");
            Console.WriteLine(" ");
            foreach (Type auxiliar in interfaces)
            {
                Console.WriteLine("Nome = " + auxiliar.FullName);
            }
            Console.WriteLine(" ");
        }

        //Método para imprimir os atributos da classe 
        private static void ImprimeAtributos(Type pTipo)
        {
            Console.WriteLine(" $$$$$ Imprimindo os atributos da classe System.type");
            Console.WriteLine(" ");
            Object[] atributos = pTipo.GetCustomAttributes(true);
            Console.WriteLine("Existe(m) " + atributos.Length + " atributos. ");
            Console.WriteLine(" ");
            foreach (Object atributo in atributos)
            {
                Console.WriteLine("Tipo = " + atributo.GetType());
            }
            Console.WriteLine(" ");
        }

        //Método para imprimir os métodos da classe 
        private static void ImprimeMetodos(Type pTipo)
        {
            Console.WriteLine(" $$$$$ Imprimindo os métodos da classe System.Type");
            Console.WriteLine(" ");
            MethodInfo[] metodos = pTipo.GetMethods();
            Console.WriteLine("Existe(m) " + metodos.Length + " métodos. ");
            Console.WriteLine("");
            foreach (MethodInfo metodo in metodos)
            {
                Console.WriteLine("Nome = " + metodo.Name);
                Console.WriteLine("É privado = " + metodo.IsPrivate);
                Console.WriteLine("É estático = " + metodo.IsStatic);
                ParameterInfo[] parametros = metodo.GetParameters();
                if (parametros.Length > 0) Console.WriteLine(" ----- Parâmetros do método ---- ");
                foreach (ParameterInfo parametro in parametros)
                {
                    Console.WriteLine("Posição = " + parametro.Position);
                    Console.WriteLine("Nome = " + parametro.Name);
                    Console.WriteLine("Tipo = " + parametro.GetType());
                    Console.WriteLine("É de entrada = " + parametro.IsIn);
                    Console.WriteLine("É de saída = " + parametro.IsOut);
                    Console.WriteLine(" ");
                }
                Console.WriteLine("======================="); Console.WriteLine(" ");
            }
        }


    }
}