world = love.physics.newWorld(0, 0)

function love.load()
	player = {}
	computer = {}
	board = {}
	score = {}
	ball = {}

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

	computer.body = love.physics.newBody(world, computer.x, computer.y, "dynamic")
	computer.shape = love.physics.newRectangleShape(computer.w / 2, computer.h / 2, computer.w, computer.h)
	computer.fixture = love.physics.newFixture(computer.body, computer.shape)

	ball.body = love.physics.newBody(world, ball.x, ball.y, "dynamic")
	ball.shape = love.physics.newCircleShape(ball.sprite:getWidth() / 2)
	ball.fixture = love.physics.newFixture(ball.body, ball.shape)

	board.topShape = love.physics.newEdgeShape(0, 0, board.w, 0)
	board.topBody = love.physics.newBody(world, 0, 45, "static")
	board.topFixture = love.physics.newFixture(board.topBody, board.topShape)

	board.bottomShape = love.physics.newEdgeShape(0, 0, board.w, 0)
	board.bottomBody = love.physics.newBody(world, 0, board.h + 35, "static")
	board.bottomFixture = love.physics.newFixture(board.bottomBody, board.bottomShape)
end

function love.update(dt)
	world:update(dt)
	-- Game logic would go here
	playerDx, playerDy = 0, 0
	computerDx, computerDy = 0, 0

	local speed = 300

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


	ball.body:setLinearVelocity(0, 300)

	player.x, player.y = player.body:getPosition()
	computer.x, computer.y = computer.body:getPosition()

	ball.x, ball.y = ball.body:getPosition()
end

function love.draw()
	love.graphics.draw(score.playerBarSprite, 0, 0)
	love.graphics.draw(score.computerBarSprite, 800, 0, nil, -1, 1)
	love.graphics.draw(board.sprite, 0, 45)
	love.graphics.draw(player.sprite, player.x, player.y - player.h / 2)
	love.graphics.draw(computer.sprite, computer.x, computer.y - computer.h / 2)
	love.graphics.draw(ball.sprite, board.w / 2 - ball.sprite:getWidth() / 2, board.h / 2)
end
