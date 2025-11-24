
function love.load()
	world = love.physics.newWorld(0, 0)
	player = {}
	computer = {}
	board = {}
	score = {}
	ball = {}
	Text = ""
 
	world:setCallbacks(beginContact, endContact, preSolve, postSolve)

	ball.sprite = love.graphics.newImage("assets/sprites/ball.png")
	ball.motion = {}
	ball.motion.sprite = love.graphics.newImage("assets/sprites/ballMotion.png")
	score.playerBarSprite = love.graphics.newImage("assets/sprites/scoreBar.png")
	score.computerBarSprite = love.graphics.newImage("assets/sprites/scoreBar.png")
	player.sprite = love.graphics.newImage("assets/sprites/player.png")
	board.sprite = love.graphics.newImage("assets/sprites/board.png")

	player.x = 50
	player.y = board.sprite:getHeight() / 2

	ball.x = 400
	ball.y = board.sprite:getHeight() / 2

	computer.sprite = love.graphics.newImage("assets/sprites/computer.png")
	computer.x = board.sprite:getWidth() - 50
	computer.y = board.sprite:getHeight() / 2

	player.w, player.h = player.sprite:getPixelDimensions()
	computer.w, computer.h = computer.sprite:getPixelDimensions()
	board.w, board.h = board.sprite:getPixelDimensions()

	player.body = love.physics.newBody(world, player.x, player.y, "dynamic")
	player.shape = love.physics.newRectangleShape(player.w / 2, player.h / 2, player.w, player.h)
	player.fixture = love.physics.newFixture(player.body, player.shape)
	player.fixture:setUserData("Player")
	player.body:setFixedRotation(true)

	computer.body = love.physics.newBody(world, computer.x, computer.y, "dynamic")
	computer.shape = love.physics.newRectangleShape(computer.w / 2, computer.h / 2, computer.w, computer.h)
	computer.fixture = love.physics.newFixture(computer.body, computer.shape)
	computer.fixture:setUserData("Computer")

	computer.body:setFixedRotation(true)

	ball.body = love.physics.newBody(world, ball.x, ball.y, "dynamic")
	ball.shape = love.physics.newCircleShape(ball.sprite:getWidth() / 2)
	ball.fixture = love.physics.newFixture(ball.body, ball.shape)
	ball.fixture:setUserData("Ball")
	
	board.topShape = love.physics.newEdgeShape(0, 0, board.w, 0)
	board.topBody = love.physics.newBody(world, 0, 45, "static")
	board.topFixture = love.physics.newFixture(board.topBody, board.topShape)
	board.topFixture:setUserData("Board Top")
	
	board.bottomShape = love.physics.newEdgeShape(0, 0, board.w, 0)
	board.bottomBody = love.physics.newBody(world, 0, board.h + 35, "static")
	board.bottomFixture = love.physics.newFixture(board.bottomBody, board.bottomShape)
	board.bottomFixture:setUserData("Board Bot")
end

function love.update(dt)
	world:update(dt)
	-- Game logic would go here
	playerDx, playerDy = 0, 0
	computerDx, computerDy = 0, 0

	speed = 300

	if love.keyboard.isDown("w") then
		playerDy = -speed
	end

	if love.keyboard.isDown("s") then
		playerDy = speed
	end

	if love.keyboard.isDown("up") then
		computerDy = -speed
	end

	if love.keyboard.isDown("down") then
		computerDy = speed
	end

	player.body:setLinearVelocity(playerDx, playerDy)
	computer.body:setLinearVelocity(computerDx, computerDy)

	player.x, player.y = player.body:getPosition()
	computer.x, computer.y = computer.body:getPosition()

	ball.x, ball.y = ball.body:getPosition()

	if string.len(Text) > 768 then-- Cleanup when 'Text' gets too long
		Text = "" -- Reset the Text variable when it exceeds the specified length
	end

	if ball.x > 800 then
		ball.body:setLinearVelocity(0, 0)
		ball.body:setPosition(400, board.sprite:getHeight() / 2)
	elseif ball.x < 0 then
		ball.body:setLinearVelocity(0, 0)
		ball.body:setPosition(400, board.sprite:getHeight() / 2)
	end



	if love.keyboard.isDown("space") then
		anglex = math.random(0, 180)
		angley = math.random(-180, 0)
		ball.body:setLinearVelocity(speed * anglex * dt, speed * angley * dt)
	end 

end

function love.draw()
	love.graphics.draw(score.playerBarSprite, 0, 0)
	love.graphics.draw(score.computerBarSprite, 800, 0, nil, -1, 1)
	love.graphics.draw(board.sprite, 0, 45)
	love.graphics.draw(player.sprite, player.x, player.y)
	love.graphics.draw(computer.sprite, computer.x, computer.y)
	love.graphics.draw(ball.sprite, ball.x, ball.y)

	love.graphics.print(Text, 10, 10)
end


function beginContact(a, b, coll)
	Persisting = 1
	local x, y = coll:getNormal()
	local textA = a:getUserData()
	local textB = b:getUserData()
-- Get the normal vector of the collision and concatenate it with the collision information
	Text = Text.."\n 1.)" .. textA.." colliding with "..textB.." with a vector normal of: ("..x..", "..y..")"
	love.window.setTitle ("Persisting: "..Persisting)

 	if textA == "Ball" or textB == "Ball" then
		anglex = speed * math.cos(anglex)
		angley = speed * math.sin(angley)
		Text = "Hello"
		ball.body:setLinearVelocity(anglex, angley)
	end

end

function endContact(a, b, coll)
	Persisting = 0
	local textA = a:getUserData()
	local textB = b:getUserData()
-- Update the Text to indicate that the objects are no longer colliding
	Text = Text.."\n 3.)" .. textA.." uncolliding with "..textB
	love.window.setTitle ("Persisting: "..Persisting)
end

function preSolve(a, b, coll)
	if Persisting == 1 then
	local textA = a:getUserData()
	local textB = b:getUserData()
-- If this is the first update where the objects are touching, add a message to the Text
		Text = Text.."\n 2.)" .. textA.." touching "..textB..": "..Persisting
	elseif Persisting <= 10 then
-- If the objects have been touching for less than 20 updates, add a count to the Text
		Text = Text.." "..Persisting
	end
	
-- Update the Persisting counter to keep track of how many updates the objects have been touching
	Persisting = Persisting + 1
	love.window.setTitle ("Persisting: "..Persisting)
end

function postSolve(a, b, coll, normalimpulse, tangentimpulse)
-- This function is empty, no actions are performed after the collision resolution
-- It can be used to gather additional information or perform post-collision calculations if needed
end
