/* GET home page. */
module.exports = {
    index: (req, res, next) => {
        res.status(200).send({ message: "Welcome to my API" });
    }
};
