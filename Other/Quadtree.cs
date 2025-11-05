using System.Collections.Generic;

public class Quadtree
{
	private const int maxObjects = 4;
	private const int maxLevels = 5;
	private int _level;
	private List<GameObject> _objects;
	private Rectangle _bounds;
	private Quadtree[] _nodes;
	public Quadtree(int level, Rectangle bounds)
	{
		_level = level;
		_objects = new List<GameObject>();
		_bounds = bounds;
		_nodes = new Quadtree[maxObjects];
	}

	public void CleanUp()
	{
		_objects.CleanUp();
		for (int i = 0; i < _nodes.Length; i++)
		{
			if (_nodes[i] != null)
			{
				_nodes[i].CleanUp();
				_nodes[i] = null;
			}
		}
	}

	private void Subdivide()
	{
		int x = _bounds.X;
		int y = _bounds.Y;
		int w = _bounds.Width;
		int h = _bounds.Height;

		_nodes[0] = new Quadtree(_level + 1, new Rectangle(x + w / 2, y, w / 2, h / 2)); // northeast
        _nodes[1] = new Quadtree(_level + 1, new Rectangle(x, y, w / 2, h / 2)); // northwest           
        _nodes[2] = new Quadtree(_level + 1, new Rectangle(x, y + h / 2, w / 2, h / 2)); // southwest
        _nodes[3] = new Quadtree(_level + 1, new Rectangle(x + w / 2, y + h / 2, w / 2, h / 2)); // southeast
    }

	private int GetIndex(Rectangle rect)
	{
		int index = -1;
		double verticalMidpoint = _bounds.X + (_bounds.Width / 2.0);
		double horizontalMidpint = _bounds.Y + (_bounds.Height / 2.0);

		bool topQuadrant = rect.Y < horizontalMidpint && rect.Y + rect.Height < horizontalMidpint;
		bool bottomQuadrant = rect.Y > horizontalMidpint;

		if (rect.X + rect.Width <= verticalMidpoint)
		{
			if (topQuadrant) index = 1; // northwest
            else if (bottomQuadrant) index = 2; // southwest
        }
		else if (rect.X > verticalMidpoint)
		{
			if (topQuadrant) index = 0; // northeast
            else if (bottomQuadrant) index = 3; // southeast
        }
		return index;
	}

	public void insert(GameObject obj)
	{
		if (obj == null) return;

		if (_nodes[0] != null)
		{
			int index = GetIndex(obj.Bounds);
			if (index != -1)
			{
				_nodes[index].insert(obj);
				return;
			}
		}
		_objects.Add(obj);

		if (_objects.Count > maxObjects && _level < maxLevels)
		{
			if (_nodes[0] == null)
				Subdivide();
            // Move objects to child nodes
            int i = 0;
			while (i < _objects.Count)
			{
				int index = GetIndex(_objects[i].Bounds);
				if (index != -1)
				{
					var moving = _objects[i];
					_objects.RemoveAt(i);
                    _nodes[index].insert(moving);
                }
				else i++;
			}
		}
	}

	public List<GameObject> Retrieve(List<GameObject> returnObjects, GameObject obj)
	{
		if (returnObjects == null)
			returnObjects = new List<GameObject>();

        int index = GetIndex(obj.Bounds);
		if (index != -1 && _nodes[0] != null)
		{
			_nodes[index].Retrieve(returnObjects, obj);
		}
        // Add all objects in this node
        returnObjects.AddRange(_objects);
		return returnObjects;
	}
}
