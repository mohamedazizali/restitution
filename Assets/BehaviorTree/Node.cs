using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    /// <summary>
    /// Énumération représentant les différents états qu'un nœud peut retourner après évaluation.
    /// RUNNING : Le nœud est en cours d'exécution.
    /// SUCCESS : Le nœud a réussi son évaluation.
    /// FAILURE : Le nœud a échoué dans son évaluation.
    /// </summary>
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    /// <summary>
    /// Classe de base pour tous les nœuds d'un arbre de comportement.
    /// Un nœud peut avoir des enfants, un parent, et stocker des données contextuelles.
    /// </summary>
    public class Node
    {
        /// <summary>
        /// L'état actuel du nœud après évaluation.
        /// </summary>
        protected NodeState state;

        /// <summary>
        /// Référence au nœud parent de ce nœud.
        /// </summary>
        public Node parent;

        /// <summary>
        /// Liste des nœuds enfants attachés à ce nœud.
        /// </summary>
        protected List<Node> children = new List<Node>();

        /// <summary>
        /// Dictionnaire pour stocker les données contextuelles spécifiques à ce nœud.
        /// </summary>
        private Dictionary<string, object> _dataContext = new Dictionary<string, object>();

        /// <summary>
        /// Constructeur par défaut qui initialise un nœud sans parent ni enfants.
        /// </summary>
        public Node()
        {
            parent = null;
        }

        /// <summary>
        /// Constructeur qui permet d'initialiser un nœud avec une liste de nœuds enfants.
        /// </summary>
        /// <param name="children">Liste des nœuds enfants à attacher à ce nœud.</param>
        public Node(List<Node> children)
        {
            foreach (Node child in children)
                _Attach(child);
        }

        /// <summary>
        /// Attache un nœud enfant à ce nœud, en définissant le nœud parent de l'enfant.
        /// </summary>
        /// <param name="node">Le nœud enfant à attacher.</param>
        private void _Attach(Node node)
        {
            node.parent = this;
            children.Add(node);
        }

        /// <summary>
        /// Évalue ce nœud et retourne son état. 
        /// Cette méthode est destinée à être redéfinie par les classes dérivées.
        /// </summary>
        /// <returns>État du nœud après évaluation, par défaut FAILURE.</returns>
        public virtual NodeState Evaluate() => NodeState.FAILURE;

        /// <summary>
        /// Stocke une donnée contextuelle dans ce nœud.
        /// </summary>
        /// <param name="key">Clé de la donnée.</param>
        /// <param name="value">Valeur de la donnée.</param>
        public void SetData(string key, object value)
        {
            _dataContext[key] = value;
        }

        /// <summary>
        /// Récupère une donnée contextuelle depuis ce nœud ou ses parents.
        /// </summary>
        /// <param name="key">Clé de la donnée à récupérer.</param>
        /// <returns>Valeur de la donnée si trouvée, sinon null.</returns>
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
        /// Efface une donnée contextuelle de ce nœud ou de ses parents.
        /// </summary>
        /// <param name="key">Clé de la donnée à effacer.</param>
        /// <returns>Vrai si la donnée a été trouvée et effacée, sinon faux.</returns>
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
