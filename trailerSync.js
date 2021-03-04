/* 
  YouTube: https://youtu.be/TEgLZFVHPEQ

  #Кастомная синхронизация трейлеров для платформы RAGE:MP JJiGolem#7069
  http://discord.gg/RqKSczZsNW - Сообщество Голема
  
	#Приватная репозитория на github
	https://github.com/JJiGolem/rage-custom-trailers-sync

  Метод костыльный, но лучше, чем вообще ничего
  Идея использовать клиент для синхры ))
*/
let trailers = {};
async function attachTrailerByVehicle(truck, trailer) {
	let reAttachInterval = setInterval(() => {
        if(mp.vehicles.exists(truck)) {
            if(truck.getVariable("TRAILER") != null){
                mp.events.call("Create::Trailer", truck, truck.position);
                if(mp.vehicles.exists(trailer)) {
                    trailer.setCanBeVisiblyDamaged(false);       //no damages
                    trailer.setCanBreak(false);                  //can break
                    trailer.setDeformationFixed();               //fixed deformation
                    trailer.setDirtLevel(0);                     //clear
                    trailer.setDisablePetrolTankDamage(true);    //disable fueltank damage
                    trailer.setDisablePetrolTankFires(true);     //disable fire fuel
                    trailer.setInvincible(true);                 //godmode
                }
                else {
                    if(reAttachInterval) clearInterval(reAttachInterval);
                }
            }
        }
        else {
            if(reAttachInterval) clearInterval(reAttachInterval);
        }
	}, 1000);
};
mp.events.add('Create::Trailer', (truck, position) => {
	if(trailers[truck.remoteId] != null) {
		setTimeout(() => {
			truck.attachToTrailer(trailers[truck.remoteId].handle, 1000);
		}, 50);
		return;
	}
  let trailer = mp.vehicles.new(mp.game.joaat(truck.getVariable("TRAILER")), position,
	{
        heading: truck.getHeading(),
        numberPlate: "TRAILER"
	});
	trailers[truck.remoteId] = trailer;
	if(trailer != null){
		setTimeout(() => {
			truck.attachToTrailer(trailer.handle, 1000);
			attachTrailerByVehicle(truck, trailer);
		}, 200);
	}
	else {
		mp.gui.chat.push("к сожалению, голем, твой трейлер не заспавнился...ты идиот");
	}
});

mp.events.add('entityStreamIn', (entity) => {
    if(entity && mp.vehicles.exists(entity)){
        if(entity.type == "vehicle"){
            if(entity.getVariable("TRAILER") != null){
                mp.events.call("Create::Trailer", entity, entity.position);
            }
        }
    }
});

mp.events.add('entityStreamOut', (entity) => {
	if(entity && mp.vehicles.exists(entity)){
        if(entity.type == "vehicle"){
            if(entity.getVariable("TRAILER") != null){
                if(trailers[entity.remoteId] != null) {
                  trailers[entity.remoteId].destroy();
                  trailers[entity.remoteId] = null;
                }
            }
        }
    }
});
