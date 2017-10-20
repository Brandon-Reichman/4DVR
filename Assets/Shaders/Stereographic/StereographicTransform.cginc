VertexInput StereographicTransform(VertexInput v)
{
	// v.vertex is already a 4-vector, but with unused "w" component equal to 1.
	// Set the w component to tangent.x instead:
	v.vertex.w = v.tangent.x;

	// Apply fdtransform 4x4 matrix
	v.vertex = mul(_fdtransform,v.vertex);

	// To project back to 3-space, we want to "drop" the w coordinate, i.e.
	// set it back to 1.0 which is ignored by Unity (but required to be there).

	v.vertex.x=v.vertex.x/(1-v.vertex.w);
	v.vertex.y=v.vertex.y/(1-v.vertex.w);
	v.vertex.z=v.vertex.z/(1-v.vertex.w);
	v.vertex.w = 1.0;

  	return v;
}