// Find the latest version of THREE on https://cdn.skypack.dev/three.
// import * as THREE from 'https://cdn.skypack.dev/pin/three@v0.134.0-dfARp6tVCbGvQehLfkdx/mode=imports/optimized/three.js';
import * as THREE from "https://cdn.jsdelivr.net/npm/three@0.121.1/build/three.module.js";
import { GLTFLoader } from "https://cdn.jsdelivr.net/npm/three@0.121.1/examples/jsm/loaders/GLTFLoader.js";
//----------------------------------------------------------------------------------------------------------------------
// Global variables
let scene = null, camera = null, renderer = null;
let aspectRatio = window.innerWidth / window.innerHeight;
const gltfLoader = new GLTFLoader().setPath('/assets/');
let pigs = [];
let frame = 0;
// ---------------------------------------------------------------------------------------------------------------------
export function Init3DWorld(aCanvas, settings, race) {
    scene = new THREE.Scene();
    scene.background = new THREE.Color("#d5d5d5");

    //Set up your scene camera
    camera = new THREE.PerspectiveCamera(50, aspectRatio, 1, 2000);
    camera.position.set(-10, 8, -5);
    camera.lookAt(0, 0, 0);

    //Setup your renderer
    const rendererParams = {canvas: aCanvas, antialias: true};
    renderer = new THREE.WebGLRenderer(rendererParams);
    renderer.setPixelRatio(aspectRatio);
    renderer.setSize(window.innerWidth*0.6, window.innerHeight*0.6);
    renderer.shadowMap.enabled = true;

    AddLight(settings);
    AddGroundPlane(settings);
    AddPigs(settings);

    window.addEventListener( 'resize', onWindowResize );

    // Animate
    requestAnimationFrame(updateFrame);
    
    setTimeout(function (){ 
        setInterval(function() { AnimateObject(settings, race); }, 1);
    }, 2000);
}
//----------------------------------------------------------------------------------------------------------------------
function updateFrame() {
    renderer.render(scene, camera);
    requestAnimationFrame(updateFrame);
}

function AddGroundPlane(settings) {
    const plane = new THREE.PlaneBufferGeometry(2*settings.n_pigs + 1, settings.map_length);
    const matOption = {color: "#964b00" }
    const mat = new THREE.MeshPhongMaterial(matOption);
    const mesh = new THREE.Mesh(plane, mat);
    mesh.rotation.x = -Math.PI / 2;
    mesh.position.set(0.5,0,settings.map_length/2 - 1);
    mesh.receiveShadow = true;
    scene.add(mesh);

    const grid = new THREE.GridHelper(settings.map_length*2, settings.map_length*2, "#ffffff", "#ffffff");
    grid.material.opacity = 0.5;
    grid.material.transparent = true;
    scene.add(grid);
}

function AddPigs(settings) {
    for (let i=0; i < settings.n_pigs; i++) {
        gltfLoader.load('Pig.json', function (object) {
            // Mesh
            pigs[i] = object.scene;
            pigs[i].receiveShadow = true;
            pigs[i].castShadow = true;
            pigs[i].position.set(i*2 - settings.n_pigs + 1.5, 0, 0);
            
            //Color
            let color = new THREE.Color('#' + (Math.random() * 0xFFFFFF << 0).toString(16).padStart(6, '0')); //Pig color
            pigs[i].traverse(function (child) {
                if (child instanceof THREE.Mesh)
                    child.material.color.set(color);
            });
            scene.add(pigs[i])
        });
    }
}

function AddLight(settings) {
    let light = new THREE.HemisphereLight("#FFF", "#444", 0.5);
    light.position.set(-10, 10, 500 * settings.map_length / 2);
    scene.add(light);

    const light_dist = settings.map_length / 10;
    for (let i = 0; i*light_dist <= settings.map_length; i++) {
        light = new THREE.PointLight(0xFFFFFF, 0.7, 800);
        light.position.set(-10, 10, i * light_dist);
        scene.add(light);
    }
}

function onWindowResize() {
    camera.aspect = window.innerWidth / window.innerHeight;
    camera.updateProjectionMatrix();

    renderer.setSize( window.innerWidth, window.innerHeight );

    render();
}

function render() {
    renderer.render( scene, camera );
}

function AnimateObject(settings, race) {
    frame += 1;
    
    let p = "x" + frame;
    let p_cam = 0;

    let finished = 0;
    for (let j in pigs) {
        if (pigs[j].position.z >= settings.map_length)
            finished++;
    }
    if (finished === settings.n_pigs) {
        return 1;
    } else {
        for (let i in pigs) {
            let name = race["Name"][i];
            
            pigs[i].position.z = race[name][p] / settings.slow;
            p_cam += pigs[i].position.z;
        }
        
        if (p_cam / settings.n_pigs > settings.map_length * 0.9){
            camera.position.set(-15, 1, settings.map_length)
            camera.lookAt(0, 0, settings.map_length);
        } else {
            camera.position.z = p_cam / settings.n_pigs - 5;
        }
    }
}