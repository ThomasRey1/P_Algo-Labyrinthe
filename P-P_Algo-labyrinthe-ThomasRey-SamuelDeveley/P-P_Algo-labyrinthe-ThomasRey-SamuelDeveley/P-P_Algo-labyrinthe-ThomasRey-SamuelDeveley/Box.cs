using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P_P_Algo_labyrinthe_ThomasRey_SamuelDeveley
{
    /// <summary>
    /// Cette class permet de créer les cases qui composerons notre labyrinthe
    /// </summary>
    class Box
    {
        //initialisation des variables
        int _locationX = 0,                 //Position x de la box
            _locationY = 0;                 //Position y de la box
                                            
        string _id;                         //id de la box

        bool _north = true,                //déterminer si la boxe à un mur à la face nord
             _south = true,                  //déterminer si la boxe à un mur à la face sud
             _west = true,                 //déterminer si la boxe à un mur à la face ouest
             _east = true,                 //déterminer si la boxe à un mur à la face est
             _isChecked = false;            //déterminer si la box à été checkée


        /// <summary>
        /// get set de l'id
        /// </summary>
        public string ID
        {
            get => _id;
            set => _id = value;
        }

        /// <summary>
        /// get set de la position x
        /// </summary>
        public int LocationX
        {
            get => _locationX;
            set => _locationX = value;
        }

        /// <summary>
        /// get set de la posiotn y 
        /// </summary>
        public int LocationY
        {
            get => _locationY;
            set => _locationY = value;
        }

        /// <summary>
        /// get set si la box à déjà été check
        /// </summary>
        public bool IsChecked
        {
            get => _isChecked;
            set => _isChecked = value;
        }

        /// <summary>
        /// get set du mur nord
        /// </summary>
        public bool North
        {
            get => _north;
            set => _north = value;
        }

        /// <summary>
        /// get set du mur sud
        /// </summary>
        public bool South
        {
            get => _south;
            set => _south = value;
        }

        /// <summary>
        /// get set du mur ouest
        /// </summary>
        public bool West
        {
            get => _west;
            set => _west = value;
        }

        /// <summary>
        /// get set du mur est
        /// </summary>
        public bool East
        {
            get => _east;
            set => _east = value;
        }

        /// <summary>
        /// Construteur permettant de créer une bix avec un id est des coordonnées
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="locationX">location x</param>
        /// <param name="locationY">location y</param>
        public Box(string id, int locationX, int locationY)
        {
            _id = id;
            _locationX = locationX;
            _locationY = locationY;
        }
        public Box()
        {

        }

    }
}
