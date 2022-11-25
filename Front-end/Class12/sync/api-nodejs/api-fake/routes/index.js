const express = require('express');
const router = express.Router();
const HomeController = require('../controllers/homeController');
const ClientsController = require('../controllers/clientsController');

/* GET home page. */
router.get('/', HomeController.index);

router.get('/clients', ClientsController.index);
router.get('/clients/:id', ClientsController.show);
router.post('/clients', ClientsController.create);
router.put('/clients/:id', ClientsController.update);
router.delete('/clients/:id', ClientsController.delete);

module.exports = router;
