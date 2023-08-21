export function json_to_array(json_file) {
    let result = [];

    for (let i in json_file) {
        result.push(json_file[i]);
    }
        
    return result[1].name;
}

//not needed -from oskar

//test