#pragma once
#include <Windows.h>
#include <d3d11.h>
#include <tchar.h>
#include <string>

#pragma warning(disable:4996)

#include "MinHook/MinHook.h"
#include "ImGui/imgui.h"
#include "ImGui/imgui_impl_win32.h"
#include "ImGui/imgui_impl_dx11.h"
#include "spoof_call.hpp"
#include "MenuVariables.h"
#include "GameFunctions.h"

#define INRANGE(x, a, b)   (x >= a && x <= b)
#define GET_BITS( x )    (INRANGE((x&(~0x20)),'A','F') ? ((x&(~0x20)) - 'A' + 0xa) : (INRANGE(x,'0','9') ? x - '0' : 0))
#define GET_BYTE( x )    (GET_BITS(x[0]) << 4 | GET_BITS(x[1]))


extern uintptr_t jmp_rbx;
extern uintptr_t ModuleBase;

extern HWND window;
extern WNDPROC oWndProc;
extern ImDrawList* gDrawList;
extern ID3D11Device* pDevice;
extern ID3D11DeviceContext* pContext;
extern ID3D11RenderTargetView* mainRenderTargetView;

extern LRESULT ImGui_ImplWin32_WndProcHandler(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam);

template <typename T>
inline MH_STATUS MH_CreateHookEx(LPVOID pTarget, LPVOID pDetour, T** ppOriginal);
uintptr_t find_pattern(const char* module_name, const char* pattern);

