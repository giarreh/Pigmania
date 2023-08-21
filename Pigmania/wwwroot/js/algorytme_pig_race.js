class Pig {
    constructor(name, speed, x, place, finish) {
        this.name = name;
        this.speed = speed;
        this.x = x;
        this.place = place;
        this.finish = finish;
    }
}

export function Race(settings, pigso) {
    let pigs = [];
    let myPig0 = new Pig(pigso["name"][0], pigso["speed"][0], 0, 0, false);
    pigs.push(myPig0);
    let myPig1 = new Pig(pigso["name"][1], pigso["speed"][1], 0, 0, false);
    pigs.push(myPig1);
    let myPig2 = new Pig(pigso["name"][2], pigso["speed"][2], 0,0, false);
    pigs.push(myPig2);
    let myPig3 = new Pig(pigso["name"][3], pigso["speed"][3], 0, 0, false);
    pigs.push(myPig3);
    
    
    let y = ["x0"];
    let tid = 0;
    let position = 0;
    let winner;
    let pigs_left;

    
    var dict = {
        "place": {},
        "Name": {}
    };

    for (let i = 0; i < settings.n_pigs; i++) {
        dict[pigs[i].name] = {};
        dict["Name"][i] = pigs[i].name;
    }

    for (let i = 0; i < settings.n_pigs; i++) {
        pigs[i].speed = pigs[i].speed/10;
    }
    
    for (let i = 0; i < settings.n_pigs; i++) {
        dict[pigs[i].name][y[tid]] = 0;
    }
    
    while (true) {
        for (let i = 0; i < settings.n_pigs; i++) {
            pigs[i].x = dict[pigs[i].name][y[tid]] + pigs[i].speed;
        }

        y.push("x" + (tid + 1));
        
        for (let i = 0; i < settings.n_pigs; i++) {
            let rand = randn_bm(0, 1, 0.5)
            if (pigs[i].speed + rand > 1)
                pigs[i].speed = pigs[i].speed + rand;
        }
        
        tid++;
        
        for (let i = 0; i < settings.n_pigs; i++) {
            dict[pigs[i].name][y[tid]] = pigs[i].x;
        }

        pigs_left = 0;

        for (let i = 0; i < settings.n_pigs; i++) {
            if (pigs[i].x >= settings.map_length * settings.slow && pigs[i].finish === false) {
                position += 1;
                pigs[i].place = position;
                dict["place"][pigs[i].name] = pigs[i].place;
                if (pigs[i].place === 1) {
                    winner = new Pig(pigs[i].name, pigs[i].speed, pigs[i].x, pigs[i].place, true)
                }
                pigs[i].finish = true;
            }
            if (pigs[i].finish === false) {
                pigs_left += 1;
            }
        }

        if (pigs_left === 0) {
            break;
        }

    }

    return dict;
}

function RandomNumb(pigNum) {
    let ranNu = Math.floor((Math.random() * pigNum) + 1);
   
    return ranNu;
}

function choose_pig(settings, json_file) {
    let result = [];

    const pigs = json_file.sort(() => Math.random() - Math.random()).slice(0, settings.n_pigs)

    for (const i in pigs) {
        result.push(json_file[i]);
    }

    return result;
}

