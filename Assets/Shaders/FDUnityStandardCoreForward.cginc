// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

#ifndef UNITY_STANDARD_CORE_FORWARD_INCLUDED
#define UNITY_STANDARD_CORE_FORWARD_INCLUDED

// UNITY BUILT-INS
#include "UnityStandardConfig.cginc"

// OUR CODE
#include "FDUnityStandardCore.cginc"

VertexOutputForwardBase vertBase (VertexInput v) {
    return vertForwardBase(v);
}

VertexOutputForwardAdd vertAdd (VertexInput v) {
    return vertForwardAdd(v);
}

half4 fragBase (VertexOutputForwardBase i) : SV_Target { return fragForwardBaseInternal(i); }
half4 fragAdd (VertexOutputForwardAdd i) : SV_Target { return fragForwardAddInternal(i); }

#endif // UNITY_STANDARD_CORE_FORWARD_INCLUDED
