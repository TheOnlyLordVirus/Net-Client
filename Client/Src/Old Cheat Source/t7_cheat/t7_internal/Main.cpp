#include "Main.h"

uintptr_t jmp_rbx;
uintptr_t ModuleBase;

HWND window;
WNDPROC oWndProc;
ImDrawList* gDrawList;
ID3D11Device* pDevice = NULL;
ID3D11DeviceContext* pContext = NULL;
ID3D11RenderTargetView* mainRenderTargetView;
MenuVariables mVars;

void InitImGui()
{
	ImGui::CreateContext();
	ImGuiIO& io = ImGui::GetIO();
	io.ConfigFlags = ImGuiConfigFlags_NoMouseCursorChange;
	ImGui_ImplWin32_Init(window);
	ImGui_ImplDX11_Init(pDevice, pContext);
}


LRESULT __stdcall WndProc(const HWND hWnd, UINT uMsg, WPARAM wParam, LPARAM lParam) {

	ImGuiIO& io = ImGui::GetIO();
	POINT mPos;
	GetCursorPos(&mPos);
	ScreenToClient(window, &mPos);
	ImGui::GetIO().MousePos.x = mPos.x;
	ImGui::GetIO().MousePos.y = mPos.y;

	if (uMsg == WM_KEYUP)
	{
		if (wParam == VK_INSERT)
		{

			if (mVars.bMenuOpen)
				io.MouseDrawCursor = true;
			else
				io.MouseDrawCursor = false;
		}
	}
	//0x120 - numBones in dobj
	if (mVars.bMenuOpen) {
		ImGui_ImplWin32_WndProcHandler(hWnd, uMsg, wParam, lParam);
		return true;
	}


	return CallWindowProc(oWndProc, hWnd, uMsg, wParam, lParam);
}

void DrawESP() {
	

	char buf[400];
	float screen[2];
	for (int i = 0; i < 2048; i++) {
		uintptr_t dobj = Com_GetClientDObj(i, 0);
		if (dobj != 0) {
			DObjAnimMat* mat = DObjGetRotTransArray(dobj);
			if (mat != 0) {
				if (CG_WorldPosToScreenPos(0, (float*)&mat->trans, screen)) {
					//snprintf(buf, 400, "%i %p (%.2f, %.2f, %.2f)", *reinterpret_cast<short*>(dobj + 0x120), dobj, mat[0].trans.x, mat[0].trans.y, mat[0].trans.z);
					uintptr_t temp = *reinterpret_cast<uintptr_t*>(dobj + 0x18);
					if (temp != 0) {
						temp = *reinterpret_cast<uintptr_t*>(temp);
						if (temp != 0) {
							temp = *reinterpret_cast<uintptr_t*>(temp);
							if (temp != 0) {
								if (strstr((char*)(temp), "_mpc_body")) {
									//snprintf(buf, 400, "%i - %s", *reinterpret_cast<short*>(dobj + 0x120), (char*)(temp));
									//gDrawList->AddText(ImVec2(screen[0], screen[1]), 0xFFFFFFFF, buf);
									
									for (int aa = 0; aa < *reinterpret_cast<short*>(dobj + 0x120);aa++) {
										if (CG_WorldPosToScreenPos(0, (float*)&mat[aa].trans, screen)) {
											snprintf(buf, 400, "%i", aa);
											gDrawList->AddText(ImVec2(screen[0], screen[1]), 0xFFFFFFFF, buf);
										}
									}

								}
							}
						}
					}
				}
			}
		}
	}

	//float mm[3];
	//float screen[2];

	//if (UI_WorldPosToLUIPos(0, mm, screen)) {
	//	
	//}

}

void DrawMenu() {
	if (mVars.bMenuOpen) {
		if (ImGui::Begin("MCC Cheat", &mVars.bMenuOpen)) {

		}
	}

	ImGui::PushStyleVar(ImGuiStyleVar_WindowBorderSize, 0.0f);
	ImGui::PushStyleVar(ImGuiStyleVar_WindowPadding, { 0.0f, 0.0f });
	ImGui::PushStyleColor(ImGuiCol_WindowBg, { 0.0f, 0.0f, 0.0f, 0.0f });
	ImGui::Begin("test", nullptr, ImGuiWindowFlags_NoTitleBar | ImGuiWindowFlags_NoInputs);

	ImGui::SetWindowPos(ImVec2(0, 0), ImGuiCond_Always);
	ImGui::SetWindowSize(ImVec2(ImGui::GetIO().DisplaySize.x, ImGui::GetIO().DisplaySize.y), ImGuiCond_Always);


	//draw here

	gDrawList = ImGui::GetWindowDrawList();

	DrawESP();

	ImGui::End();
	ImGui::PopStyleColor();
	ImGui::PopStyleVar(2);

}

HRESULT(__stdcall* Present_Stub)(IDXGISwapChain* pSwapChain, UINT SyncInterval, UINT Flags);
HRESULT __stdcall Present_Hook(IDXGISwapChain* pSwapChain, UINT SyncInterval, UINT Flags) {
	if (!mVars.bInitiateMenu) {

		if (SUCCEEDED(pSwapChain->GetDevice(__uuidof(ID3D11Device), (void**)&pDevice)))
		{
			pDevice->GetImmediateContext(&pContext);
			DXGI_SWAP_CHAIN_DESC sd;
			pSwapChain->GetDesc(&sd);
			window = sd.OutputWindow;
			ID3D11Texture2D* pBackBuffer;
			pSwapChain->GetBuffer(0, __uuidof(ID3D11Texture2D), (LPVOID*)&pBackBuffer);
			pDevice->CreateRenderTargetView(pBackBuffer, NULL, &mainRenderTargetView);
			pBackBuffer->Release();
			oWndProc = (WNDPROC)SetWindowLongPtr(window, GWLP_WNDPROC, (LONG_PTR)WndProc);
			InitImGui();
			mVars.bInitiateMenu = true;
		}
	}

	ImGui_ImplDX11_NewFrame();
	ImGui_ImplWin32_NewFrame();
	ImGui::NewFrame();

	if (GetAsyncKeyState(VK_INSERT) & 1) {
		mVars.bMenuOpen = !mVars.bMenuOpen;
	}

	DrawMenu();

	ImGui::Render();

	pContext->OMSetRenderTargets(1, &mainRenderTargetView, NULL);
	ImGui_ImplDX11_RenderDrawData(ImGui::GetDrawData());
	return Present_Stub(pSwapChain, SyncInterval, Flags);
}

BOOL WINAPI DllMain(HMODULE hModule, DWORD Reason, LPVOID lpVoid) {

	if (Reason == DLL_PROCESS_ATTACH) {
		MH_Initialize();

		ModuleBase = (uintptr_t)GetModuleHandle(0);

		const char* BaseModule = "BlackOps3.exe";

		uintptr_t present = find_pattern(("dxgi.dll"), ("55 57 41 56 48 8D 6C 24 ?? 48 81 EC ?? ?? ?? ?? 48 8B 05 ?? ?? ?? ?? 48 33 C4 48 89 45 60"));

		jmp_rbx = find_pattern(BaseModule, "FF E3");


		MH_CreateHookEx((void*)(present - 10), &Present_Hook, &Present_Stub);

		MH_EnableHook(MH_ALL_HOOKS);
	}

	if (Reason == DLL_PROCESS_DETACH) {
		SetWindowLongPtr(window, GWLP_WNDPROC, (LONG_PTR)oWndProc);
		MH_DisableHook(MH_ALL_HOOKS);
		MH_Uninitialize();
	}

	return TRUE;
}


template <typename T>
inline MH_STATUS MH_CreateHookEx(LPVOID pTarget, LPVOID pDetour, T** ppOriginal) {
	return MH_CreateHook(pTarget, pDetour, reinterpret_cast<LPVOID*>(ppOriginal));
}




uintptr_t find_pattern(const char* module_name, const char* pattern)
{
	const auto get_module_size = [=](uintptr_t module_base)
	{
		return reinterpret_cast<PIMAGE_NT_HEADERS>(module_base + reinterpret_cast<PIMAGE_DOS_HEADER>(module_base)->e_lfanew)->OptionalHeader.SizeOfImage;
	};
	const auto module_start = (uintptr_t)GetModuleHandle(module_name);
	if (module_start != 0ULL)
	{
		const auto module_end = module_start + get_module_size(module_start);

		const char* pattern_current = pattern;
		uintptr_t current_match = NULL;

		MEMORY_BASIC_INFORMATION64 page_information = {};
		for (auto current_page = reinterpret_cast<unsigned char*>(module_start); current_page < reinterpret_cast<unsigned char*>(module_end); current_page = reinterpret_cast<unsigned char*>(page_information.BaseAddress + page_information.RegionSize))
		{
		VirtualQuery(reinterpret_cast<LPCVOID>(current_page), reinterpret_cast<PMEMORY_BASIC_INFORMATION>(&page_information), sizeof(MEMORY_BASIC_INFORMATION));
		if (page_information.Protect == PAGE_NOACCESS)
			continue;

		if (page_information.State != MEM_COMMIT)
			continue;

		if (page_information.Protect & PAGE_GUARD)
			continue;

		for (auto current_address = reinterpret_cast<unsigned char*>(page_information.BaseAddress); current_address < reinterpret_cast<unsigned char*>(page_information.BaseAddress + page_information.RegionSize - 0x8); current_address++)
		{
			if (*current_address != GET_BYTE(pattern_current) && *pattern_current != '\?') {
				current_match = 0ULL;
				pattern_current = pattern;
				continue;
			}

			if (!current_match)
				current_match = reinterpret_cast<uintptr_t>(current_address);

			pattern_current += 3;
			if (pattern_current[-1] == NULL)
				return current_match;
		}
		}
	}

	return 0ULL;
}


