using Microsoft.VisualBasic;
using System.Windows.Forms;

namespace Queue_2_sentinels
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            cola = new Cola();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            button1.Click+=button1_Click;
            button2.Click+=button2_Click;
            button3.Click+=button3_Click;

            listBox1.Items.Clear();




        }


        //este metodo lo vamos a llamar cuando encolamos y cuando desencolamos,
        //cuando hacemos ver() no porque cuando hacemos ver() no modificamos nada
        private void Mostrar(Cola pCola)  //recibo como parámetro una estructura cola porque Mosrtar tiene que saber que cola mostrar
        {
            listBox1.Items.Clear();


            //declaro e instancio cola auxiliar
            Cola auxCola = new Cola();

            //desencolar de la cola original, mostrar y encolar en la auxiliar
            //agarro la cola q llega aca como parámetro


            //desencolé el nodo de la cola original y lo apunto con auxNodo
            Nodo auxNodo = pCola.Desencolar();  //este nodo que desencolé lo tengo q agarrar

            while (auxNodo != null)  //si entra aca es xq la cola al menos tenia un nodo
            {

                //muestro el id del nodo desencolado en el listbox
                //una vez desencolado el nodo y guardado en auxnodo, muestro en el listbox
                listBox1.Items.Add(auxNodo.id); //uso el add, que xque agrega uno atras del otro 
                                                //agrego el id de ese nodo

                //una vez que lo mostré, ahora ese nodo lo quiero encolar a la cola auxiliar
                auxCola.Encolar(auxNodo.id);   //encolé en la cola auxiliar el nodo desencolado de la cola original

                //desencolo el proximo nodo
                auxNodo = pCola.Desencolar();

            }
            //restituimos la cola original
            auxNodo = auxCola.Desencolar();


            //Despues de hacer todo para mostrar, tengo que
            //asegurarme que el estado de la cola original quede inmaculado porque el mostrar no
            //debe afectar los nodos q tiene la estructura
            while (auxNodo != null)
            {
                //encolo en la cola original el nodo desencolado de la cola auxiliar
                pCola.Encolar(auxNodo.id);
                //desencolo el proximo nodo 
                auxNodo = auxCola.Desencolar();


            }


        }



        Cola cola;
        //genero la cola acá y la instancio dentro del formulario
        private void button1_Click(object sender, EventArgs e)
        {  //cuando le hagan click al boton, yo quioro usar una cola y encolarle
            cola.Encolar(Interaction.InputBox("Ingrese id del nodo"));
            Mostrar(cola);
        }

        private void button2_Click(object sender, EventArgs e)
        {

            //como me devuelve null o un nodo vamos a "atajarlo" en una variable auxiliar
            Nodo auxNodo = cola.Desencolar();
            if (auxNodo == null)
            {
                MessageBox.Show("no hay + nodos en la cola ");
            }
            else
            {
                MessageBox.Show($"El nodo desencolado es: {auxNodo.id}");
            }
            Mostrar(cola);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //me devuelve un nodo, o nulo
            Nodo auxNodo = cola.Ver();   //CUANDO VEA EL NODO QUE ME DEVUELVE , VA A APUNTAR A NULL,NO VA A APUNTAR AL SEGUNDO NODO, NO SE PUEDE METER ADENTRO DE LA ESTRUCTURA PORQUE LE DEVOLVÍ UN "CLON" CUYO SIGUIENTE ES NULL        
            if (auxNodo == null)
            {
                MessageBox.Show("Te comento que . . .  No hay nodos en la cola ");
            }
            else
            {
                MessageBox.Show($"El nodo que esta para ser desencolado es: {auxNodo.id}");
            }

        }

        private void button3_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }
    }

    public class Nodo
    {
        public Nodo(string pId = "", Nodo pSig = null)
        {
            id = pId; Sig = pSig;
        }



        public string id { get; set; }
        public Nodo Sig { get; set; }  //esto seria el puntero al siguiente elemento


    }
    public class Cola
    {
        Nodo NCP;  // "nodo centinela primero": este nodo es parte de la cola pero no conforma uno de los elementos de la cola,
                   // si no que me sirve para saber y apuntar cual es el primer nodo que conforma la estructura

        Nodo NCU;

   //en el constructor de la cola instancio NCP y NCU
        public Cola()
        {

            //como está vacía ambos apuntan a null
            NCP = new Nodo();
            NCU = new Nodo();
            NCP.Sig = null;
            NCU.Sig = null;

        }

        public void Encolar(string pId)   //recibe el id
        {
            //si la cola esta vacía agrego un nuevo nodo
            if (NCP.Sig == null)  //en este caso NCP es un objeto para crearlo instancie un nodo
            {
                NCP.Sig = new Nodo(pId);
                NCU.Sig = NCP.Sig;  //AMBOS APUNTAN AL MISMO LUGAR, xque es el primer nodo 
            }
            else //si la cola posee al menos 1 nodo
            {

                //UBICO EL ÚLTIMO NODO
                Nodo auxNodo = NCU.Sig;
                //AGARRO ESE ULTIMO NODO Y LE CUELGO UN NUEVO NODO
                auxNodo.Sig = new Nodo(pId);    //le paso el id que me dio el usuario
               
                //ASIGNAMOS AL CENTINELA ULTIMO EL NUEVO ULTIMO NODO RECIEN AGREGADO
                NCU.Sig = auxNodo.Sig;

            }
        }

        public Nodo Desencolar()
        {
            if (NCP.Sig == null)
            {
                return NCP.Sig;
            }
            else
            {    //tomo el nodo que quiero retornar para apuntar al nodo primero
                // apunto con auxNodo al primero nodo de la cola
                Nodo auxNodo = NCP.Sig;
                //si auxNodo apunta al primero entonces apunto al que le sigue al primero
                NCP.Sig = auxNodo.Sig;   //o si no NCP.Sig.Sig que tambien hace que apunte al segundo nodo  
                                         //hago que centinela primero apunte al nuevo primer nodo, que seria el que estaba en segundo lugar



                //si el centinela primero apunta a null por estar en la cola vacia, el NCU tambien debe apuntar a nulo
                if (NCP.Sig == null) NCU.Sig = null;



                //rompo la referencia del primer nodo con el segundo
                auxNodo.Sig = null;
                return auxNodo;

            }


        }

        public Nodo Ver()   //me dice cual es el nodo que esta para ser desencolado pero no quitarlo
        {
            //dos escenarios: si cola vacia devuelve nulo y si tiene nodos entonces devuelve el primero

            //si esta vacia retorno nulo
            if (NCP.Sig == null)
            {
                return NCP.Sig;
            }
            else
            {
                //retornar un nuevo nodo cuyo id va a ser el primer nodo.id
                return new Nodo(NCP.Sig.id);
            }


        }

       

       

    }


}