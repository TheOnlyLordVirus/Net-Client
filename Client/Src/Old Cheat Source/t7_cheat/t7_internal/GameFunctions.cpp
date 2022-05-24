#include "GameFunctions.h"



uintptr_t Com_GetClientDObj(int handle, int localClientNum) {
	auto com_getclientdobj = reinterpret_cast<uintptr_t(*)(int, int)>(ModuleBase + 0x214EBA0);
	return com_getclientdobj(handle, localClientNum);
}

DObjAnimMat* DObjGetRotTransArray(uintptr_t dobj) {
	auto dobjgetrottransarray = reinterpret_cast<DObjAnimMat * (*)(uintptr_t)>(ModuleBase + 0x233E4F0);
	return dobjgetrottransarray(dobj);
}

bool CG_WorldPosToScreenPos(int localClientNum, const float* origin, float* outpos) {
	//return spoof_call((const void*)(ModuleBase + 0x1F80674E), ((bool(*)(int, const float*, float*))(ModuleBase + 0x2819EE0)), localClientNum, origin, outpos);
	return spoof_call((const void*)(ModuleBase + 0x1F80674E), ((bool(*)(int, const float*, float*))(ModuleBase + 0x574140)), localClientNum, origin, outpos);
}
