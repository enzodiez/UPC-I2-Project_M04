using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightLib
{
    public class Position
    {
        double x; // coordenada X (2D)
        double y; // coordenada Y (2D)
        double posicioninicialx;
        double posicioninicialy;

        public Position(double x, double y)
        {
            this.x = x;
            this.y = y;
            this.posicioninicialx = x; //Guardamos la posicion inicial para poder reiniciar la posicion del avion una sola vez (inicial)
            this.posicioninicialy = y;
        }

        public double GetX()
        // getter del atributo x
        { return x; }
        public void SetX(double x)
        { this.x = x; }
        public double GetY()
        // getter del atributo y
        { return y; }
        public void SetY(double y)
        { this.y = y; }
        public double GetInicialX()
        // getter del atributo x
        { return posicioninicialx; }
        
        public double GetInicialY()
        // getter del atributo y
        { return posicioninicialy; }
        
        public double Distancia(Position b)
        // retorna la distancia entre los dos Postion
        {
            double resultado = Math.Sqrt((x - b.x) * (x - b.x) + (y - b.y) * (y - b.y));
            return resultado;
        }      
        public void Restart() //cambiamos el atributo posicion a la inicial sencillamente:
        {
            this.x = posicioninicialx; //xi
            this.y = posicioninicialy;//yi
        }
    }
}