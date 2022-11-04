const gamaStudents = ["Paula", "Maria", "Estela", "Clara"]

//Acessar informações num array
console.log(gamaStudents[3]) //"Clara"

// Operador spread (...)
const xpStudents = [...gamaStudents, "Simara"]

console.log(xpStudents)

//Metodos de iteração
//Map
xpStudents.map(student => console.log(student))


//

//Filter
const numbersFilter = [34, 45, 67, 90, 55, 76]

const oddNumbers = numbersFilter.filter(number => number%2 !=0)
console.log(oddNumbers)

const evenNumbers = numbersFilter.filter(number => number%2 ==0)
console.log(evenNumbers)


const products = ["geladeira", "fogao", "cama", "mesa"]

//find

const findBed = products.find(product => product === "cama")
console.log(findBed)

const findTable = products.find(product => product === "mesa")
console.log(findTable)

//sort - ordenação 

const numbersOrdenadosCrescente = numbers.sort()
console.log(numbersOrdenadosCrescente)

const numbersOrdenadosDecrescente = numbers.sort((a,b)=> b-a)
console.log(numbersOrdenadosDecrescente)


//reduce - reduz nosso array a um resultado de uma operação matemática

const numbers = [1,34,35]

const sum = numbers.reduce((lastValue, currentValue) => {
    return lastValue + currentValue
})

console.log(sum) //75