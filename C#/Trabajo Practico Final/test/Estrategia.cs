
using System;
using System.Collections.Generic;
namespace DeepSpace
{

	class Estrategia
	{
		
		
		public String Consulta1( ArbolGeneral<Planeta> arbol)
		{
			return "Implementar";
		}


		public String Consulta2( ArbolGeneral<Planeta> arbol)
		{
			return "Implementar";
		}


		public String Consulta3( ArbolGeneral<Planeta> arbol)
		{
			
			return "Implementar";
		}
		
		public Movimiento CalcularMovimiento(ArbolGeneral<Planeta> arbol)
		{
			// Buscar nodo del user
			Planeta nodoUser = arbol.getDatoRaiz();

			// Buscar nodo del bot
			Planeta nodoBot = arbol.getDatoRaiz();
			// Posibilidades

			// 1. Estan en el mismo sub arbol y tiene mayor jerarquia

			// 2. Estan en el mismo sub arbol y tiene menor jerarquia

			// 3. Sub arboles distintos --> El bot debe desplazarse hacia la raiz
			
			//Implementar
			
			return null;
		}
	}
}
