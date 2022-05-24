#pragma once
#include "Main.h"

struct vec4_t {
	float x;
	float y;
	float z;
	float w;
};

struct vec3_t {
	float x;
	float y;
	float z;
	float w;
};

struct DObjAnimMat
{
	vec4_t quat;
	vec3_t trans;
	float transWeight;
};


uintptr_t Com_GetClientDObj(int handle, int localClientNum);
DObjAnimMat* DObjGetRotTransArray(uintptr_t dobj);
bool CG_WorldPosToScreenPos(int localClientNum, const float* worldPos, float* outScreenPos);
