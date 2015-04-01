-----------------------------------------------------------------------------
--Love Game Maker
--An easy-to-use tool for developing games using the marvelous LOVE2D framework!
--NOTE: Currently under HEAVY DEVELOPMENT!
-----------------------------------------------------------------------------

-- Load the wxLua module, does nothing if running from wxLua, wxLuaFreeze, or wxLuaEdit
package.cpath = package.cpath..";./?.dll;./?.so;../lib/?.so;../lib/vc_dll/?.dll;../lib/bcc_dll/?.dll;../lib/mingw_dll/?.dll;"
require("wx")

frame = nil

function main()

    -- create the frame windows
    frame = wx.wxFrame( wx.NULL, 1, "Untitled - Love Game Maker",
                        wx.wxDefaultPosition, wx.wxSize(450, 450),
                        wx.wxDEFAULT_FRAME_STYLE )
    
    local buttonSizer = wx.wxBoxSizer( wx.wxHORIZONTAL )
    treeCtrl = wx.wxTreeCtrl(frame,2,wx.wxPoint(0,0))
    buttonSizer:Add(treeCtrl)
    --pnl = wx.wxPanel(frame, 1);

    local rootnd = treeCtrl:AddRoot("Untitled Project")

    local spritend = treeCtrl:AppendItem(rootnd, "Sprites", 0, 1)
    local objectnd = treeCtrl:AppendItem(rootnd, "Objects", 0, 1)
    local bgnd = treeCtrl:AppendItem(rootnd, "Backgrounds", 0, 1)
    local soundnd = treeCtrl:AppendItem(rootnd, "Sounds", 0, 1)
    local scriptnd = treeCtrl:AppendItem(rootnd, "Scripts", 0, 1)
    local roomnd = treeCtrl:AppendItem(rootnd, "Rooms", 0, 1)
    treeCtrl:AppendItem(spritend, "Sprite 0", 0, 1)

    treeCtrl:Expand(rootnd)

    -- show the frame window
    frame:Show(true)
end

main()

-- Call wx.wxGetApp():MainLoop() last to start the wxWidgets event loop,
-- otherwise the wxLua program will exit immediately.
-- Does nothing if running from wxLua, wxLuaFreeze, or wxLuaEdit since the
-- MainLoop is already running or will be started by the C++ program.
wx.wxGetApp():MainLoop()
