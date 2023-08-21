function callRace(ran_pigs){
    const settings = {n_pigs:4, map_length: 1000, slow: 100}
    const raco = Race(settings, ran_pigs);
   
    return raco;
}

//not needed -from oskar