using System.ComponentModel.Design;

namespace Cola_super
{
    public partial class Form1 : Form
    {
        //defino dos colas, donde voy a sacar de comprar para colocar en cobrar 
        Cola comprar;
        Cola cobrar;

        //como genero aleatoriamente las personas genero un string de nombres 
        string[] nombres = { "Ariel", "Ana", "Abel", "Mabel", "Gary", "Sol","Gary", "Pedro", "Cecilia", "Maria", "Mirko" };
        Random r;  //variable r

        //estas dos variables las uso para ir acumulando las compras y cobros
        decimal tComprado, tCobrado;




        public Form1()
        {
            InitializeComponent();

            //creo el objeto random
            r = new Random();

            //inicializo las dos colas  
            comprar = new Cola();
            cobrar = new Cola();
            tComprado = 0;
            tCobrado = 0;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            listBox2.Items.Clear();


        }

        private void Mostrar(Cola pCola, ListBox pListBox)
        {
            pListBox.Items.Clear();

            //declaro e instancio la col auxiliar
            Cola auxCola = new Cola();
            //desencolo el nodo de la cola original y lo apunto con aux nodo
            Persona auxNodo = pCola.Desencolar();

            while (auxNodo != null)
            {
                // Mostramos el id del nodo desencolado en el listbox
                pListBox.Items.Add(auxNodo.ToString());
                // Encolamos en la cola auxiliar el nodo desencolado de la cola original
                auxCola.Encolar(auxNodo.Nombre, auxNodo.Valor);
                // Desencolo el próximo Nodo
                auxNodo = pCola.Desencolar();

            }
            //restituimos la cola original
            auxNodo = auxCola.Desencolar();
            while (auxNodo != null)
            {
                // Encolamos en la cola original el nodo desencolado de la cola auxiliar
                pCola.Encolar(auxNodo.Nombre, auxNodo.Valor);
                // Desencolo el próximo Nodo
                auxNodo = auxCola.Desencolar();
            }


        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //la listbox1 va a mostrar las personas con los valores q compraron 
        //cuando active el boton comprar se va a poner a funcionar timer1, este
        //temporizador tiene programado una cantidad de segundos que la puedo
        //cambiar de la barrita, cada vez que transcurre esta cant de segundos
        //, va a generar aleatoriamente una persona y le va a poner un valor aleatorio
        //,va  a cargar e ir mostrando la cola de compras
        }

        //el otro listbox, cuando active el boton cobrar, el timer2 cada vez q cambie la cant.
        //de segundos, va a desencolar una persona de la lista1 y va  a encolar en la lista2
        //que es la lista de cobrados. Me suma el importe cobrado a la caja de texto 2 y se lo resta de la caja de texto 1



        private void timer1_Tick(object sender, EventArgs e)
        {

            //calculamos un valor de compra aleatorio entre 100 y 1000
            decimal compra = Convert.ToDecimal(r.Next(1000, 10001));


            //encolo en la cola comprar pasando al metodo encolar el nombre de la persona y el valor q
            //ue obtuvimos aleatoriamente en compra

            //a la cola comprar le encolo y me pide el nombre de la persona y el valor
            //el nombre lo tomo del vector nombres y la posicion es aletoria entre 0 y 9 xq tengo diez nobmres
            //y compra es el aleatoerio q se genero entre 1000 y 10000
            comprar.Encolar(nombres[r.Next(10)], compra);
            
            //a mostrar le mando la cola q quiero mostrar y en qué listbox 
            Mostrar(comprar, listBox1);
            tComprado += compra;  //acumulo de valor de compra
            textBox1.Text = $"$ {tComprado}";


            //todo esto sucede cada vez que se genere una persona aleatorea 


        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            //verifica que exista una persona para cobrarle
            if (comprar.Ver() != null)
            {

                //si hay algo entra aqui, agarra la lista de compras y desencola, saca el primero
                Persona auxPersona = comprar.Desencolar();  //una vez q lo guarde en auxPersona lo encolo en la cola de cobrar
                cobrar.Encolar(auxPersona.Nombre, auxPersona.Valor);
                Mostrar(cobrar, listBox2);
                Mostrar(comprar, listBox1);
                tComprado -= auxPersona.Valor; //resto el valor en tComprado y lo asigno a la caja de texto
                tCobrado += auxPersona.Valor;
                textBox1.Text = $"$ {tComprado}";
                textBox2.Text = $"$ {tCobrado}";

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "COBRAR")
            {
                timer2.Start();
                button2.Text = "PARAR DE COBRAR";
            }
            //los timers son los q compran y cobran
            else
            {
                timer2.Stop();
                button2.Text = "COBRAR";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //cuando activo el boton comprar, funciona el timer1 que tiene programado
            //una cant de segundos que la puedo cambiar desde el schrol bar, cada vez
            //que transcurre esa cant. de segundos va a generar aleatoriamente una persona
            //con un valor aleatoreo y carga la cola de compras.   


            //cuando le hacen click pregunta si la leyenda es comprar
            if (button1.Text == "COMPRAR")
            {   //si dice "comprar", activa el timer 1 y le cambia la leyenda al boton
                timer1.Start();
                button1.Text = "PARAR DE COMPRAR";
            }
            else
            {
                timer1.Stop();
                button1.Text = "COMPRAR";
            }

        }




        //el scroll bar esta configurado para que vaya desde 200 hasta 2209
        //
        private void hScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }
        private void hScrollBar2_Scroll(object sender, ScrollEventArgs e)
        {

        }




        private void hScrollBar1_ValueChanged(object sender, EventArgs e)
        {
            //muestro esa etiqueta que me dice cuantos segundos tengo configurados
            label1.Text = $"{Convert.ToDecimal(hScrollBar1.Value) / 1000m} seg.";
            
            //le paso el valor del scrollbar al interval del timer (la propiedad interval del timer es la que configura cuanto tiempo tiene que pasar hasta que el timer desencadena el evento tick  )
            timer1.Interval = hScrollBar1.Value;


            //evento cambio de valor del scroll bar
            //al label que tengo que me dice cuantos segundos tiene configurado
            //le paso el valor del scroll bar al interval del timer
            

            //el interval del timer configuta cuanto tiene q pasar hasta q el timer desencadena el evento tick 
            //o sea si son dos segundos, va a tardar dos segundos desde que activo el timer1 hasta que se ejecute el evento timer 1 tick 
        }

        private void hScrollBar2_ValueChanged(object sender, EventArgs e)
        {
            label2.Text = $"{Convert.ToDecimal(hScrollBar2.Value) / 1000m} seg";


            timer2.Interval = hScrollBar2.Value;
        }


        

       

    }
    }


    public class Cola
    {
        Persona NCP;
        Persona NCU;

        public Cola() { NCP = new Persona(); NCU = new Persona(); NCP.Siguiente = null;  }


        public void Encolar(string pNombre, decimal pValor)
        {
            if (NCP.Siguiente == null)
            {   //al momento de encolar creo una persona y le paso el nombre y el valor
                NCP.Siguiente = new Persona(pNombre, pValor);   

                NCU.Siguiente = NCP.Siguiente;
            }
            else
            {
                Persona auxNodo = NCU.Siguiente;
                auxNodo.Siguiente = new Persona(pNombre, pValor);

                NCU.Siguiente = auxNodo.Siguiente;
            }
        }


        public Persona Desencolar()
        {
            if (NCP.Siguiente == null)
            {
                return NCP.Siguiente;
            }
            else
            {    
                Persona auxNodo = NCP.Siguiente;
                
                NCP.Siguiente = auxNodo.Siguiente; 
               
                if (NCP.Siguiente == null) NCU.Siguiente = null;

                auxNodo.Siguiente = null;
                return auxNodo;

            }


        }

        public Persona Ver()
        {
            if (NCP.Siguiente == null)
            {
                return NCP.Siguiente;
            }

            else
            {  //le paso un clon del nodo del q esta primero 
                return new Persona(NCP.Siguiente.Nombre, NCP.Siguiente.Valor);
            }
        }


    }


//serian los "nodos" de la cola
    public class Persona
    {
        public Persona(string pNombre = "", decimal pValor = 0m, Persona pSiiguiente = null)
        {
            //inicializacion de los valores, se le pasa a las propiedades los valores por el contructor     
            Nombre = pNombre;
            Valor = pValor;
            Siguiente = pSiiguiente; 

        }

        public string Nombre { get; set; }
        public decimal Valor { get; set; }

        public Persona Siguiente { get; set; }


        //sobrescribi el meotodo tostring para que cuando le pida       
        //el tostring a una persona directamente ya me de el nombre y el valor  
        //me viene mas facil que andar concatenando adhoc cada vez que tengo que mostrar eso
        public override string ToString()
        {
            return $"{Nombre} - $ {Valor}";
        }


    }


