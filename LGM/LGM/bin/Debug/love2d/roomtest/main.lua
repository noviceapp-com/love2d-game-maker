--Room Object orientation example
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
	local rm = require('Room0')
	table.insert(rooms,rm())
	local rm = require('Room1')
	table.insert(rooms,rm())
end

function love.quit()
    love.filesystem.remove("error.txt")
end

function love.keypressed(key)
	if key == " " then
		local obj = require('Object0')
		table.insert(rooms[currentrm+1].objects,obj(0,table.getn(rooms[currentrm+1].objects)*48,2))
		obj.i = 8
	elseif key == "e" then
		local obj = require('Object1')
		table.insert(rooms[currentrm+1].objects,obj(0,table.getn(rooms[currentrm+1].objects)*48,2))
		obj.i = 5
		obj.k = 9
	elseif key == "r" then
		table.remove(rooms[currentrm+1].objects,table.getn(rooms[currentrm+1].objects))
	elseif key == "left" then
		if currentrm > 0 then
			currentrm = currentrm - 1
		end
	elseif key == "right" then
		if table.getn(rooms)-1 > currentrm then
			currentrm = currentrm + 1
			--rooms[currentrm+1].objects = {}
		end
	end
end

function love.update()
	rooms[currentrm+1]:update()
end

function love.draw()
	love.graphics.print("Room Object Orientation example",164,0)
	love.graphics.print("Object Count: " .. table.getn(rooms[currentrm+1].objects),164,16)
	love.graphics.print("Press space to add objects of type " .. '"' .. "object0" .. '"',164,32)
	love.graphics.print("Press e to add objects of type " .. '"' .. "object1" .. '"',164,48)
	love.graphics.print("Press r to remove objects",164,64)
	love.graphics.print("Press right to go to the next room",164,80)
	love.graphics.print("Press left to go to the previous room",164,96)
	love.graphics.print("In " .. rooms[currentrm+1].tp,164,112)
	
	rooms[currentrm+1]:draw()
end