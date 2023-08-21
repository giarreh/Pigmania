
function choose_pigs(settings, json_file) {
   
    let result = [];

    const pigs = json_file.sort(() => Math.random() - Math.random()).slice(0, settings.n_pigs)

    for (const i in pigs) {
        result.push(json_file[i]);
    }

    return result;
}

function ranAmount(start, end) {
    const rndInt = Math.floor(Math.random() * end) + start
    console.log(rndInt)
    return rndInt;
}


function SendAmount(){
    let page1, pig1, pig2, pig3, pig4;
    page1 = sessionStorage.getItem("bettamount");
    pig1 = sessionStorage.getItem("name1");
    pig2 = sessionStorage.getItem("name2");
    pig3 = sessionStorage.getItem("name3");
    pig4 = sessionStorage.getItem("name4");
    sessionStorage.setItem("amountbett", page1);
    sessionStorage.setItem("one", pig1);
    sessionStorage.setItem("two", pig2);
    sessionStorage.setItem("three", pig3);
    sessionStorage.setItem("four", pig4);
    
    
}

function input(pig1){
    sessionStorage.setItem("one", pig1);
}

function output(){
    return sessionStorage.getItem("one");
}

