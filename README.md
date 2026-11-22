# Flight Simulator V2 — Proyecto Informática 2 (M04)

Simulador de tráfico aéreo desarrollado en C# (.NET / Windows Forms) para la asignatura de Informática 2 (EETAC - UPC).

**Integrantes del grupo:**
* Enzo Díez
* Víctor Crespo
* David Peñalver
* Manel Ruiz

---

## Estructura de la solución

* **`FlightLib`**: Biblioteca de clases con la lógica principal del simulador (`FlightPlan`, `FlightPlanList`), cálculo de trayectorias, detección de conflictos e historial de estados.
* **`SimulatorInterface`**: Interfaz gráfica de usuario (Windows Forms) con mapa interactivo, gestión de sesiones y visualización de aeronaves en tiempo real.

## Funcionalidades principales (Versión 2)

* **Gestión de usuarios:** Inicio de sesión y registro conectados a base de datos local SQLite (`usuarios.db`).
* **Carga y creación de vuelos:** Importación desde fichero (`FlightPlans.txt`) o introducción manual de nuevos planes de vuelo.
* **Control de simulación:** Avance paso a paso, simulación automática por ciclos y reinicio al estado inicial.
* **Historial (`Undo`):** Almacenamiento de estados anteriores mediante pila (`Stack`) para retroceder turnos de simulación.
* **Guardado y carga de simulaciones:** Exportación e importación completa del estado de la simulación en curso, incluyendo parámetros e historial.
* **Seguridad e inspección:** Detección de conflictos actuales y futuros según la distancia de seguridad, y consulta o modificación de velocidades al hacer clic sobre cualquier aeronave en el mapa.

## Cómo ejecutar el proyecto

1. Abrir el archivo de solución **`UPC-I2-Project_M04.slnx`** con Visual Studio.
2. Verificar que **`SimulatorInterface`** está establecido como proyecto de inicio.
3. Compilar la solución (`Compilar -> Recompilar solución`) y ejecutar.