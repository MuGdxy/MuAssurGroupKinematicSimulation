using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAGKS
{
    public class DrivingLink:Bar
    {
        private Vector2 posA = Vector2.zero;
        private double l;
        private double phi0;
        private double omega0;
        private double alpha0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="l">length</param>
        /// <param name="phi0">Initial angular</param>
        /// <param name="omega0">Initial angular velocity</param>
        /// <param name="alpha0">Initial angular acceleration</param>
        public DrivingLink(double l,double phi0,double omega0,double alpha0)
        {
            this.l = l;
            this.phi0 = phi0;
            this.omega0 = omega0;
            this.alpha0 = alpha0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pos">pos</param>
        /// <param name="l">length</param>
        /// <param name="phi0">Initial angular</param>
        /// <param name="omega0">Initial angular velocity</param>
        /// <param name="alpha0">Initial angular acceleration</param>
        public DrivingLink(Vector2 pos,double l, double phi0, double omega0, double alpha0)
            :this(l,phi0,omega0,alpha0)
        {
            posA = pos;
        }

        public override I_Output GetOutput(double t)
        {
            I_Output o = new I_Output();
            o.posA = posA;
            o.vA = Vector2.zero;
            o.aA = Vector2.zero;
            o.posB = new Vector2(
                posA.x + l * Math.Cos(phi0 + omega0 * t), 
                posA.y + l * Math.Sin(phi0 + omega0 * t));
            o.vB = new Vector2(
                -omega0 * l * Math.Sin(phi0 + omega0 * t), 
                omega0 * l * Math.Cos(phi0 + omega0 * t));
            o.aB = new Vector2(
                -omega0 * omega0 * l * Math.Cos(phi0 + omega0 * t), 
                -omega0 * omega0 * l * Math.Sin(phi0 + omega0 * t));
            o.phi = phi0;
            o.omega = omega0;
            o.alpha = alpha0;
            return o;
        }
    }
}
