using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// �num�ration repr�sentant les diff�rents �tats qu'un n�ud peut retourner apr�s �valuation.
    /// RUNNING : Le n�ud est en cours d'ex�cution.
    /// SUCCESS : Le n�ud a r�ussi son �valuation.
    /// FAILURE : Le n�ud a �chou� dans son �valuation.
    /// </summary>
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    /// <summary>
    /// Classe de base pour tous les n�uds d'un arbre de comportement.
    /// Un n�ud peut avoir des enfants, un parent, et stocker des donn�es contextuelles.
    /// </summary>
    public class Node
    {
        /// <summary>
        /// L'�tat actuel du n�ud apr�s �valuation.
        /// </summary>
        protected NodeState state;

        /// <summary>
        /// R�f�rence au n�ud parent de ce n�ud.
        /// </summary>
        public Node parent;

        /// <summary>
        /// Liste des n�uds enfants attach�s � ce n�ud.
        /// </summary>
        protected List<Node> children = new List<Node>();

        /// <summary>
        /// Dictionnaire pour stocker les donn�es contextuelles sp�cifiques � ce n�ud.
        /// </summary>
        private Dictionary<string, object> _dataContext = new Dictionary<string, object>();

        /// <summary>
        /// Constructeur par d�faut qui initialise un n�ud sans parent ni enfants.
        /// </summary>
        public Node()
        {
            parent = null;
        }

        /// <summary>
        /// Constructeur qui permet d'initialiser un n�ud avec une liste de n�uds enfants.
        /// </summary>
        /// <param name="children">Liste des n�uds enfants � attacher � ce n�ud.</param>
        public Node(List<Node> children)
        {
            foreach (Node child in children)
                _Attach(child);
        }

        /// <summary>
        /// Attache un n�ud enfant � ce n�ud, en d�finissant le n�ud parent de l'enfant.
        /// </summary>
        /// <param name="node">Le n�ud enfant � attacher.</param>
        private void _Attach(Node node)
        {
            node.parent = this;
            children.Add(node);
        }

        /// <summary>
        /// �value ce n�ud et retourne son �tat. 
        /// Cette m�thode est destin�e � �tre red�finie par les classes d�riv�es.
        /// </summary>
        /// <returns>�tat du n�ud apr�s �valuation, par d�faut FAILURE.</returns>
        public virtual NodeState Evaluate() => NodeState.FAILURE;

        /// <summary>
        /// Stocke une donn�e contextuelle dans ce n�ud.
        /// </summary>
        /// <param name="key">Cl� de la donn�e.</param>
        /// <param name="value">Valeur de la donn�e.</param>
        public void SetData(string key, object value)
        {
            _dataContext[key] = value;
        }

        /// <summary>
        /// R�cup�re une donn�e contextuelle depuis ce n�ud ou ses parents.
        /// </summary>
        /// <param name="key">Cl� de la donn�e � r�cup�rer.</param>
        /// <returns>Valeur de la donn�e si trouv�e, sinon null.</returns>
        public object GetData(string key)
        {
            object value = null;
            if (_dataContext.TryGetValue(key, out value))
                return value;

            Node node = parent;
            while (node != null)
            {
                value = node.GetData(key);
                if (value != null)
                    return value;
                node = node.parent;
            }
            return null;
        }

        /// <summary>
        /// Efface une donn�e contextuelle de ce n�ud ou de ses parents.
        /// </summary>
        /// <param name="key">Cl� de la donn�e � effacer.</param>
        /// <returns>Vrai si la donn�e a �t� trouv�e et effac�e, sinon faux.</returns>
        public bool ClearData(string key)
        {
            if (_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }

            Node node = parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                    return true;
                node = node.parent;
            }
            return false;
        }
    }
}
