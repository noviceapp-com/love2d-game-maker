--File generated using Love Game Maker (https://github.com/Radfordhound/love2d-game-maker)
class = require 'middleclass'

rooms = {}
currentrm = 0

function love.load()
	love.filesystem.setIdentity("LGMtestgame")
	local success = love.filesystem.newFile('error.txt')
	success:open('w') -- Open the file for writing
	if success:isOpen() then
		success:write(' ')
		success:close() -- Close writing and save our data so we can open it for reading
		local message, byteCount = success:read()
		print(message)
	end
	
	
end

function love.quit()
    love.filesystem.remove("error.txt")
end

function love.keypressed(key)
	--
end

function love.update()
	rooms[currentrm+1]:update()
end

function love.draw()
	love.graphics.print("Room Object Orientation example",164,0)
	love.graphics.print("In " .. rooms[currentrm+1].tp,164,112)
	
	rooms[currentrm+1]:draw()
end