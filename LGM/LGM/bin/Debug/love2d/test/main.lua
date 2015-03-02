Objects = {id=0,x=7,y=5}

function Objects.Move(self,newx,newy)
   self.x = newx
   self.y = newy
end

Obj1 = {id=0,x=7,y=8}

function love.update()
	Obj1.x = Obj1.x + 1
end

function love.draw()
	love.graphics.print("X: " ..Obj1.x .." , Y: " .. Obj1.y,0,0)
end