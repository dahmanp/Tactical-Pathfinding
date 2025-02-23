using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Dijkstra
{
    class NodeRecord : IComparable<NodeRecord>
    {
        public Node node;
        public Connection connection;
        public float costSoFar;

        public int CompareTo(NodeRecord other)
        {
            if (other == null)
            {
                return 1;
            }
            return (int)(costSoFar - other.costSoFar);
        }
    }

    class PathfindingList
    {
        List<NodeRecord> nodeRecords = new List<NodeRecord>();

        public void add(NodeRecord n)
        {
            nodeRecords.Add(n);
        }

        public void remove(NodeRecord n)
        {
            nodeRecords.Remove(n);
        }

        public NodeRecord smallestElement()
        {
            nodeRecords.Sort();
            return nodeRecords[0];
        }

        public int length()
        {
            return nodeRecords.Count;
        }

        public bool contains(Node node)
        {
            foreach (NodeRecord n in nodeRecords)
            {
                if (n.node == node)
                {
                    return true;
                }
            }

            return false;
        }

        public NodeRecord find(Node node)
        {
            foreach (NodeRecord n in nodeRecords)
            {
                if (n.node == node)
                {
                    return n;
                }
            }

            return null;
        }

    }

    public static List<Connection> pathfind(Graph graph, Node start, Node goal)
    {
        NodeRecord startRecord = new NodeRecord();
        startRecord.node = start;
        startRecord.connection = null;
        startRecord.costSoFar = 0;

        PathfindingList open = new PathfindingList();
        open.add(startRecord);
        PathfindingList closed = new PathfindingList();

        NodeRecord current = new NodeRecord();
        while (open.length() > 0)
        {
            current = open.smallestElement();

            if (current.node == goal)
            {
                break;
            }

            List<Connection> connections = graph.getConnections(current.node);

            foreach (Connection connection in connections)
            {
                Node endNode = connection.getToNode();
                float endNodeCost = current.costSoFar + connection.getCost();

                NodeRecord endNodeRecord = new NodeRecord();

                if (closed.contains(endNode))
                {
                    continue;
                }
                else if (open.contains(endNode))
                {
                    endNodeRecord = open.find(endNode);
                    if (endNodeRecord != null && endNodeRecord.costSoFar < endNodeCost)
                    {
                        continue;
                    }
                }

                else
                {
                    endNodeRecord = new NodeRecord();
                    endNodeRecord.node = endNode;
                }

                endNodeRecord.costSoFar = endNodeCost;
                endNodeRecord.connection = connection;

                if (!open.contains(endNode))
                {
                    open.add(endNodeRecord);
                }
            }
            open.remove(current);
            closed.add(current);
        }

        if (current.node != goal)
        {
            return null;
        }
        else
        {
            List<Connection> path = new List<Connection>();

            while (current.node != start)
            {
                path.Add(current.connection);
                Node fromNode = current.connection.getFromNode();
                current = closed.find(fromNode);
            }

            path.Reverse();
            return path;
        }

    }
}