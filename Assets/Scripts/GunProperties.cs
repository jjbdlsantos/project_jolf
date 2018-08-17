using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunProperties : MonoBehaviour {

	[SerializeField]
	private float _size;
	public float size {
		get {return _size;}
	}

	[SerializeField]
	private Color _colour;
	public Color colour {
		get {return _colour;}
	}

	[SerializeField]
	private int _magazine;
	public int magazine {
		get {return _magazine;}
	}

	[SerializeField]
	private int _fireRate;
	public int fireRate {
		get {return _fireRate;}
	}

	[SerializeField]
	private int _fireMode;
	public int fireMode {
		get {return _fireMode;}
	}

	[SerializeField]
	private float _bulletWeight;
	public float bulletWeight {
		get {return _bulletWeight;}
	}

	[SerializeField]
	private float _bulletVelocity;
	public float bulletVelocity {
		get {return _bulletVelocity;}
	}
}
