module.exports = {
    index: (req, res, next) => {
        res.status(200).send({ message: 'Bem-vindo ao projeto Radar' });
    }
}