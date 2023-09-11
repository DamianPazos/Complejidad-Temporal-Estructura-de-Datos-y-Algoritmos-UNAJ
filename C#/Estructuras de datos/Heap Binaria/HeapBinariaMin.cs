namespace Heap_Binaria
{
    public class HeapBinariaMin
    {
        // Atributos
        private int capacidad; // Capacidad que va a tener el heap
        private int[] elementos; // Array donde se almacenan los elementos

        // Constructor - Pide como parametro la capacidad total de la heap
        public HeapBinariaMin(int capacidad)
        {
            this.capacidad = capacidad;
            elementos = new int[this.capacidad];
        }

        // Metodos
        private bool estaLleno()
        {
            return elementos.Count() >= capacidad; 
        }

        public void insert(int dato)
        {
            if (!estaLleno())
            {
                int posicion = elementos.Count() - 1;

                if (posicion > 0)
                {
                    elementos[posicion] = dato;
                    ordenamientoMin(posicion);
                }
                else
                {
                    elementos[0] = dato;
                }
            }
        }

        public void ordenamientoMin(int posicionOrdenar)
        {
            int datoAuxiliar;
            
            if (elementos[posicionOrdenar] < elementos[(posicionOrdenar-1)/2])
            {
                datoAuxiliar = elementos[(posicionOrdenar - 1) / 2];
                elementos[elementos[(posicionOrdenar -1) / 2]] = elementos[posicionOrdenar];
                elementos[(posicionOrdenar)] = datoAuxiliar;
                ordenamientoMin((posicionOrdenar - 1) / 2);
            }
        }

        public int deleteMin()
        {
            int valorDelete = elementos[0];
            elementos[0] = elementos[elementos.Count()-1];
            // Metodo para ordenar Heap - Build Heap
            return valorDelete;
        }
    }
}