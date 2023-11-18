
using System;
using System.Collections.Generic;
namespace DeepSpace
{

	class Estrategia
	{
		/// <summary>
		/// Calcula la distancia entre el nodo perteneciente al bot y la raiz, devolviendola en un string
		/// </summary>
		/// <param name="arbol"> Estado del juego </param>
		/// <returns> Texto con la distancia entre el bot y la raiz (centro)</returns>
		public String Consulta1( ArbolGeneral<Planeta> arbol)
		{
			List<Planeta> caminoBot = new List<Planeta>();

			bool BuscarCamino(ArbolGeneral<Planeta> arbolBusqueda, List<Planeta> caminoPlanetas)
			{
				if (arbolBusqueda != null)
				{
					caminoPlanetas.Add(arbolBusqueda.getDatoRaiz());

					if (arbolBusqueda.getDatoRaiz().EsPlanetaDeLaIA())
					{
						return true;
					}
				}

				foreach (ArbolGeneral<Planeta> arbolHijo in arbolBusqueda.getHijos())
				{
					if (BuscarCamino((ArbolGeneral<Planeta>)arbolHijo, caminoPlanetas))
					{
						return true;
					}
				}

				caminoPlanetas.RemoveAt(caminoPlanetas.Count - 1);

				return false;
			}

			BuscarCamino(arbol, caminoBot);

			return $"Distancia entre Planeta Central y ubicacion del bot: {caminoBot.Count - 1} planetas ";
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="arbol"></param>
		/// <returns></returns>
		public String Consulta2(ArbolGeneral<Planeta> arbol)
		{

			List<Planeta> caminoBot = new List<Planeta>();

			void BuscarDescendientesBot(ArbolGeneral<Planeta> arbolBusqueda,List<Planeta> listaPlanetas, bool esDecendienteBot)
			{
				if (arbolBusqueda != null)
				{
					if (esDecendienteBot)
                    {
						listaPlanetas.Add(arbolBusqueda.getDatoRaiz());
                    }
				}

				if (arbolBusqueda.getDatoRaiz().EsPlanetaDeLaIA() || esDecendienteBot)
                {
					foreach (ArbolGeneral<Planeta> arbolHijo in arbolBusqueda.getHijos())
                    {
						BuscarDescendientesBot((ArbolGeneral<Planeta>)arbolHijo, listaPlanetas, true);
                    }
                }
				else
                {
					foreach (ArbolGeneral<Planeta> arbolHijo in arbolBusqueda.getHijos())
					{
						BuscarDescendientesBot((ArbolGeneral<Planeta>)arbolHijo, listaPlanetas, false);
					}
				}
			}

			BuscarDescendientesBot(arbol, caminoBot, false);
			
			return $"El listado de planetas decendientes al bot es: {caminoBot.ToString()} ";
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="arbol"></param>
		/// <returns></returns>
		public String Consulta3(ArbolGeneral<Planeta> arbol)
		{
			if (arbol == null)
			{
				return "No hay sistema de planetas";
			}

			Cola<ArbolGeneral<Planeta>> colaAux = new Cola<ArbolGeneral<Planeta>>();
			colaAux.encolar(arbol);
			colaAux.encolar(null);

			int poblacionTotal = 0;
			int poblacionNivel = 0;
			int planetasPorNivel = 0;
			int niveles = 1;
			string texto = "";

			while (!colaAux.esVacia())
            {
				ArbolGeneral<Planeta> arbolAux = colaAux.desencolar();
				if (arbolAux != null)
                {
					poblacionTotal += arbolAux.getDatoRaiz().Poblacion();
					poblacionNivel += arbolAux.getDatoRaiz().Poblacion();
					planetasPorNivel++;
					if (arbolAux.getHijos().Count > 0)
					{
						foreach (ArbolGeneral<Planeta> arbolHijo in arbolAux.getHijos())
						{
							colaAux.encolar(arbolHijo);
						}
					}
                }
				else
                {
					texto += $"El promedio en el {niveles} es {(float)poblacionNivel / planetasPorNivel}\n";
					poblacionNivel = 0;
					planetasPorNivel = 0;
					niveles++;
					if (!colaAux.esVacia())
                    {
						colaAux.encolar(null);
                    }
                }

			}

			return $"La poblacion total es de {poblacionTotal}\n{texto}";
		}
		
		public Movimiento CalcularMovimiento(ArbolGeneral<Planeta> arbol)
		{

			// Buscar nodo del user
			List<Planeta> caminoUser = new List<Planeta>();


			// Buscar nodo del bot
			List<Planeta> caminoBot = new List<Planeta>();


			bool BuscarCamino(ArbolGeneral<Planeta> arbolBusqueda, List<Planeta> caminoPlanetas, string player)
            {
				if(arbolBusqueda != null)
                {
					caminoPlanetas.Add(arbolBusqueda.getDatoRaiz());

					if (arbolBusqueda.getDatoRaiz().EsPlanetaDeLaIA() && player == "bot")
                    {
						return true;
                    }
					if (arbolBusqueda.getDatoRaiz().EsPlanetaDelJugador() && player == "user")
                    {
						return true;
                    }
                }


				foreach (ArbolGeneral<Planeta> arbolHijo in arbolBusqueda.getHijos())
                {
					if (BuscarCamino((ArbolGeneral<Planeta>)arbolHijo, caminoPlanetas, player))
                    {
						return true;
                    }
                }

				caminoPlanetas.RemoveAt(caminoPlanetas.Count - 1);

				return false;
            }

			BuscarCamino(arbol, caminoBot, "bot");
			BuscarCamino(arbol, caminoUser, "user");

			// Aplicamos una busqueda que se corte cuando encontramos ambos nodos

			Planeta planetaCercanoBot = null;
			Planeta planetaDestino = null;

			foreach (Planeta planeta in caminoUser)
            {
				if (planeta.EsPlanetaDeLaIA())
                {
					planetaCercanoBot = planeta;
                }
				if (planetaCercanoBot != null && planetaDestino == null && !planeta.EsPlanetaDeLaIA())
                {
					planetaDestino = planeta;
                }
            }

			if (planetaCercanoBot == null)
            {
				planetaCercanoBot = caminoBot[caminoBot.Count - 1];
				planetaDestino = caminoBot[caminoBot.Count - 2];
			}

			if (planetaCercanoBot.Poblacion() < planetaDestino.Poblacion() + 5)
            {

				List<Planeta> caminoHumanoBot = new List<Planeta>(caminoUser);
				caminoHumanoBot.Reverse();
				caminoHumanoBot.Remove(caminoHumanoBot[caminoHumanoBot.Count - 1]);
				caminoHumanoBot.AddRange(caminoBot);

				void AgregarNodosBots(ArbolGeneral<Planeta> estado, List<Planeta> lista)
                {

					if (estado.getDatoRaiz() != null && estado.getDatoRaiz().EsPlanetaDeLaIA() && !lista.Contains(estado.getDatoRaiz()))
                    {
						lista.Add(estado.getDatoRaiz());
                    }

					foreach (ArbolGeneral<Planeta> arbolHijo in estado.getHijos())
                    {
						AgregarNodosBots(arbolHijo, lista);
                    }
                }

				AgregarNodosBots(arbol, caminoHumanoBot);

				Planeta planetaBotMaxPoblacion = null;
				int indexCamino = -1;

				for (int i = 0; i < caminoHumanoBot.Count; i++)
				{
					if (caminoHumanoBot[i].EsPlanetaDeLaIA() && planetaBotMaxPoblacion == null)
					{
						planetaBotMaxPoblacion = caminoHumanoBot[i];
						indexCamino = i;
					}
					if (caminoHumanoBot[i].EsPlanetaDeLaIA() && planetaBotMaxPoblacion != null && planetaBotMaxPoblacion.Poblacion() < caminoHumanoBot[i].Poblacion())
                    {
						planetaBotMaxPoblacion = caminoHumanoBot[i];
						indexCamino = i;
                    }
                }

				Planeta destinoPoblacion = caminoHumanoBot[indexCamino - 1];

				return new Movimiento(planetaBotMaxPoblacion,destinoPoblacion);

            }

			return new Movimiento(planetaCercanoBot,planetaDestino);
		}
	}
}
