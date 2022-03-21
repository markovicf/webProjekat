export class Bolnica{
    constructor(){
        this.kont=null;
        this.final=null;
        this.ad=null;
    }

    crtaj(host)
    {
        this.kont = document.createElement("div");
        this.kont.className="GlavniMeni";
        host.appendChild(this.kont);

        var divOpcije = document.createElement("div");
        divOpcije.className="Opcije";
        this.kont.appendChild(divOpcije);

        var btnZakazi = document.createElement("button");
        btnZakazi.className="dugmeOpcije";
        btnZakazi.innerHTML="Zakazi";
        divOpcije.appendChild(btnZakazi);
        btnZakazi.onclick=()=>this.SetUpMenu(host);

        var btnPregled =document.createElement("button");
        btnPregled.onclick=()=>this.Pregledaj(host);
        btnPregled.className="dugmeOpcije";
        btnPregled.innerHTML="Pregledaj";
        divOpcije.appendChild(btnPregled);
    }

    SetUpMenu(host)
    {
        fetch("https://localhost:5001/Bolnica/Gradovi",{
            method: "GET"
        }).then(p=>{
            if(p.ok)
            {
                this.ObrisiSadrzaj(host,".GlavniMeni","Sporedni");
                var sporedni = document.querySelector(".Sporedni");
                var btnHome = document.createElement("button");
                btnHome.className="dugmeHome";
                btnHome.innerHTML='<ion-icon name="arrow-back-circle"></ion-icon>';
                sporedni.appendChild(btnHome);
                btnHome.onclick=()=>this.Home(host);

                var za = document.createElement("div");
                za.className="Zajedno";
                sporedni.appendChild(za);

                var izbori = document.createElement("div");
                izbori.className="Izbori";
                za.appendChild(izbori);

                var izbor = document.createElement("div");
                izbor.className="Izbor";
                izbori.appendChild(izbor);
                p.json().then(data=>{
                    //console.log(data);

                    this.napraviSelect(izbor,data,"Grad");
                    var sGrad = document.querySelector(".SelectGrad");
                    sGrad.onchange=()=>this.UzmiBolnice();

                    this.napraviSelect(izbor,data,"Bolnica");
                    var sBolnica = document.querySelector(".SelectBolnica");
                    sBolnica.onchange=()=>this.UzmiOdeljenja();

                    this.napraviSelect(izbor,data,"Odeljenje");
                    var sOd = document.querySelector(".SelectOdeljenje");
                    sOd.onchange=()=>this.UzmiDoktore();

                    this.napraviSelect(izbor,data,"Doktor");

                    var divZakazi = document.createElement("div");
                    divZakazi.className="divZakazi";
                    izbori.appendChild(divZakazi);
    

                   var vreme = document.createElement("input");
                   vreme.type="date";
                   vreme.className="Vreme";
                   divZakazi.appendChild(vreme);

                    var btnProveri = document.createElement("button");
                    btnProveri.className="dugmeProveri";
                    btnProveri.innerHTML="Termin";
                    divZakazi.appendChild(btnProveri);
                    btnProveri.onclick=()=>this.ProveriDatum();

                   
                    



                    //ovde home button i proslediti mu hosta iz parametra btn.onclick=()=>nekaFunk(host);
                    //nekaFunk=ObrisiSadrzaj Reverse+crtaj(hostt) host iz parametra
                })   
            }
        })
    
    }

    ObrisiSadrzaj(host,s1,s2)
    {
        var telo = document.querySelector(s1);
        if(telo!=null)
        {
        var parent = telo.parentNode;
        parent.removeChild(telo);

        var drugi = document.createElement("div");
        drugi.className=s2;
        host.appendChild(drugi);
        }
    }

    Home(host)
    {
        this.ObrisiSadrzaj(host,".Sporedni","GlavniMeni");

        var glavni = document.querySelector(".GlavniMeni");

        var divOpcije = document.createElement("div");
        divOpcije.className="Opcije";
        glavni.appendChild(divOpcije);

        var btnZakazi = document.createElement("button");
        btnZakazi.className="dugmeOpcije";
        btnZakazi.innerHTML="Zakazi";
        divOpcije.appendChild(btnZakazi);
        btnZakazi.onclick=()=>this.SetUpMenu(host);

        var btnPregled =document.createElement("button");
        btnPregled.onclick=()=>this.Pregledaj(host);
        btnPregled.className="dugmeOpcije";
        btnPregled.innerHTML="Pregledaj";
        divOpcije.appendChild(btnPregled);
    }

    napraviSelect(host,data,ime)
    {
        var temp=["Bolnica","Odeljenje","Doktor"]; 

        var div = document.createElement("div");
        div.className="divSelect";
        host.appendChild(div);

        let l = document.createElement("label");
        l.innerHTML=ime;
        l.className="prveLabele";
        div.appendChild(l);

        let se = document.createElement("select");
        se.className="Select"+ime;
        se.classList.add("Select");
        if(temp.includes(ime)==true)
        {
            se.setAttribute("disabled","disabled");
        }
        div.appendChild(se);
        let o = document.createElement("option");
        o.value="";
        o.innerHTML="Izaberi";
        o.className="Options";
        se.appendChild(o);
        if(ime=="Grad")
        {
        let op;
        
        var gradovi=[];
        data.forEach(p=>{
            if(gradovi.includes(p.adresa)==false)
            {
                gradovi.push(p.adresa);
            }
        })
        //console.log(gradovi);

        gradovi.forEach(p=>{
                op = document.createElement("option");
                op.value=p;
                op.innerHTML = p;
                op.className="Options";
                se.appendChild(op);
                    })
                }
    }

    UzmiBolnice()
    {
        let s = document.querySelector(".SelectBolnica").removeAttribute("disabled");
        var ops = document.querySelectorAll(".OptionsBolnica");
        if(ops!=null)
        {
            ops.forEach(el=>el.remove());
        }
        let grad = document.querySelector(".SelectGrad").value;
        //console.log(grad);
        fetch("https://localhost:5001/Bolnica/PreuzmiBolnicu/"+ grad,{
            method: "GET"
        }).then(p=>{
            if(p.ok)
            {
                p.json().then(data=>{
                    //console.log(data);
                    var s = document.querySelector(".SelectBolnica");
                    data.forEach(p=>{
                        let op = document.createElement("option");
                       // console.log(s);
                      // console.log(op);
                        op.value=p.id;
                        op.innerHTML=p.naziv;
                        op.className="OptionsBolnica";
                        op.classList.add="Options";
                        s.appendChild(op);
                    })

                })
            }
        })

    }

    UzmiOdeljenja()
    {
       let s = document.querySelector(".SelectOdeljenje").removeAttribute("disabled");
       var ops = document.querySelectorAll(".OptionsOdeljenje");
       if(ops!=null)
       {
           ops.forEach(el=>el.remove());
       }
       let od = document.querySelector(".SelectBolnica").value;
      // console.log(od);
       fetch("https://localhost:5001/Odeljenje/PreuzmiOdeljenje/"+ od,{
           method: "GET"
       }).then(p=>{
           if(p.ok)
           {
               p.json().then(data=>{
                  // console.log(data);
                   var s = document.querySelector(".SelectOdeljenje");
                   data.forEach(p=>{
                       let op = document.createElement("option");
                      // console.log(s);
                      // console.log(op);
                       op.value=p.id;
                       op.innerHTML=p.naziv;
                       op.className="OptionsOdeljenje";
                       op.classList.add="Options";
                       s.appendChild(op);
                   })

               })
           }
       })

    }

    UzmiDoktore()
    {
        let s = document.querySelector(".SelectDoktor").removeAttribute("disabled");
        var ops = document.querySelectorAll(".OptionsDoktor");
        if(ops!=null)
        {
            ops.forEach(el=>el.remove());
        }
        let doca = document.querySelector(".SelectOdeljenje").value;
        //console.log(doca);
        fetch("https://localhost:5001/Doktor/PreuzmiDocu/"+ doca,{
            method: "GET"
        }).then(p=>{
            if(p.ok)
            {
                p.json().then(data=>{
                   // console.log(data);
                    var s = document.querySelector(".SelectDoktor");
                    data.forEach(p=>{
                        let op = document.createElement("option");
                        //console.log(s);
                       // console.log(op);
                        op.value=p.id;
                        op.innerHTML=p.ime+" "+p.prezime;
                        op.className="OptionsDoktor";
                        op.classList.add="Options";
                        s.appendChild(op);
                    })

                })
            }
        })

    }


    ProveriDatum()
    {
        
        var za = document.querySelector(".Sporedni");
        var proba1=document.querySelector(".Dugmad");
        if(proba1!=null)
        {
            var parent = proba1.parentNode;
            parent.removeChild(proba1);
        }


        var doca = document.querySelector(".SelectDoktor").value;
        var bolnica = document.querySelector(".SelectBolnica").value;
        var od = document.querySelector(".SelectOdeljenje").value;
        var grad = document.querySelector(".SelectGrad").value;

        console.log(doca);
        console.log(bolnica);
        console.log(od);
        console.log(grad);

        if(grad=="" || bolnica=="" || od=="" || grad=="")
        {
            alert("Izaberite sve elemente!");
        }
        else{
        var dugmad=document.querySelector(".Dugmad");
        var datum = document.querySelector(".Vreme").value;
        
        var selektovan = new Date(datum);
        var sad = new Date();

        if(selektovan>sad)
        {
            var dugmad = document.createElement("div");
            dugmad.className="Dugmad";
            za.appendChild(dugmad);

            this.Podaci();
        for(let i=1;i<9;i++)
        {
            var btn = document.createElement("button");
            let br = i+7;
            btn.className="btn"+br;
            btn.classList.add("btn");
            btn.innerHTML=br+":00";
            dugmad.appendChild(btn);

            var novDatum = new Date(datum);
            novDatum.setHours(novDatum.getHours()+br-1);

            let vreme = novDatum.toString();
            console.log(vreme);
            const myArray = vreme.split(":");
            let word = myArray[0];


            var doca = document.querySelector(".SelectDoktor").value;

            var request = new XMLHttpRequest();
            request.open('GET',"https://localhost:5001/Spoj/Proveri/"+doca+"/"+word, false);
            request.setRequestHeader('Content-Type', 'application/json');
            request.send(null);
            
                if(request.status==200)
                {
                    console.log("Slobodno");
                    console.log(btn.className);
                }
                else if(request.status==204){
                    console.log("Zauzeto");
                    let vreme = word.substring(10,2);
                    btn.className="cancel";
                    console.log(btn.className);
                    btn.disabled=true;
                }
                

            btn.value=word;
            console.log(btn.value);

        };
        
        var temps = document.querySelectorAll(".btn");
        console.log("NIZ");
        temps.forEach(p=>{
            p.onclick=()=>this.nadjiVreme(p.value,p.className);
        })
        }   
        else
        {
            alert("Los datum");
            var div = document.querySelector(".Podaci");
            if(div!=null){
            var par = div.parentNode;
            par.removeChild(div);
            }
        }

        }
    }


    nadjiVreme(datum,klasa){
        this.final=datum;
        console.log("DATUM");
        console.log(datum);
        var klase = klasa.split(" ");
        var temp = klase[0];
        var dugme = document.querySelector("."+temp);
        dugme.className="cancel";
        dugme.setAttribute("disabled","disabled");
    }

    Podaci(){
        var second = document.querySelector(".Sporedni");

        var proba = document.querySelector(".Podaci");
        if(proba==null){
        var podaci = document.createElement("div");
        podaci.className="Podaci";
        second.appendChild(podaci);

     //   var klase = temp.split(' ');
      //  let odgovarajuceDugme = klase[0];

      //  var vreme = document.querySelector("."+odgovarajuceDugme).value;    
       // console.log(vreme);    

        /*var imelabel = document.createElement("label");
        imelabel.innerHTML="ime";
        imelabel.className="drugeLabele";
        podaci.appendChild(imelabel);*/

        var ime = document.createElement("input");
        ime.type="text";
        ime.className="Ime";
        ime.innerHTML="Unesite ime..."
        ime.classList.add("Unosi");
        ime.placeholder="Unesite ime..."
        podaci.appendChild(ime);

       /* var prezimelabel = document.createElement("label");
        prezimelabel.innerHTML="prezime";
        prezimelabel.className="drugeLabele";
        podaci.appendChild(prezimelabel);*/

        var prezime = document.createElement("input");
        prezime.type="text";
        prezime.className="Prezime";
        prezime.classList.add("Unosi");
        prezime.placeholder="Unesite prezime...";
        podaci.appendChild(prezime);

        
        /*var gmaillabel = document.createElement("label");
        gmaillabel.innerHTML="gmail";
        gmaillabel.className="drugeLabele";
        podaci.appendChild(gmaillabel);*/

        var gmail = document.createElement("input");
        gmail.type="text";
        gmail.className="Gmail";
        gmail.placeholder="Unesite mail...";
        gmail.classList.add("Unosi");
        podaci.appendChild(gmail);

        var btnZakazi = document.createElement("button");
        btnZakazi.className="dugmeZakazi";
        btnZakazi.innerHTML='<ion-icon name="add-circle-sharp"></ion-icon>';
        btnZakazi.onclick=()=>this.Unos();
        podaci.appendChild(btnZakazi);

        }
        else{
            var parent = proba.parentNode;
            parent.removeChild(proba);
            this.Podaci();
        }
        
        
    }

        
    Unos()
    {
        var ime = document.querySelector(".Ime").value;
        console.log(ime);
        var prezime = document.querySelector(".Prezime").value;
        console.log(prezime);
        var gmail = document.querySelector(".Gmail").value;
        console.log(gmail);
        var doca = document.querySelector(".SelectDoktor").value;
        console.log(doca);

        if(ime!=null && prezime!=null && doca!=null && this.isValid(gmail)==true)
        {
        fetch("https://localhost:5001/Spoj/DodajSpoj/"+this.final+"/"+doca+"/"+ime+"/"+prezime+"/"+gmail,{
            method: "POST"
            }).then(p=>{
                if(p.ok)
                {
                    p.json().then(data=>{
                        console.log(data);
                        alert("Uspesno zakazano");
                    })
                }
            })
        }
        else{
            if(ime==null)
            {
                alert("Unesite validno ime");
            }
            if(prezime==null){
                alert("Unesite validno prezime!");
            }
        }
    }

    Pregledaj(host){
        this.ObrisiSadrzaj(host,".GlavniMeni","Sporedni");
        var sporedni = document.querySelector(".Sporedni");

        var btnHome = document.createElement("button");
        btnHome.innerHTML='<ion-icon name="arrow-back-circle"></ion-icon>';
        btnHome.className="dugmeHome";
        btnHome.onclick=()=>this.Home(host);
        sporedni.append(btnHome);


        var divPretraga=document.createElement("div");
        divPretraga.className="divPretraga";
        sporedni.appendChild(divPretraga);

        var gmail = document.createElement("input");
        gmail.type = "text";
        gmail.innerHTML="Unesite mail";
        gmail.className="InputGmail";
        gmail.type="email";
        gmail.placeholder="Unesite mail...";
        divPretraga.appendChild(gmail);

        var btnTrazi = document.createElement("button");
        btnTrazi.innerHTML='<ion-icon name="search-sharp"></ion-icon>';
        btnTrazi.className="dugmeTrazi";
        divPretraga.appendChild(btnTrazi);
        btnTrazi.onclick=()=>this.Termini();

        var divTermini=document.createElement("div");
        divTermini.className="divTermini";
        sporedni.appendChild(divTermini);

        var divMenjaj=document.createElement("div");
        divMenjaj.className="divMenjaj";
        sporedni.appendChild(divMenjaj);



    }

    Termini()
    {
        var proba = document.querySelector(".divTermini");
        var proba2 = document.querySelector(".divMenjaj");
        if(proba!=null)
        {
            var parent = proba.parentNode;
            parent.removeChild(proba);
             
            var divTermini=document.createElement("div");
            divTermini.className="divTermini";
            var sporedni = document.querySelector(".Sporedni");
            sporedni.appendChild(divTermini);
        }


        if(proba2!=null)
        {
            var parent = proba2.parentNode;
            parent.removeChild(proba2);
             
            var divMenjaj=document.createElement("div");
            divMenjaj.className="divMenjaj";
            var sporedni = document.querySelector(".Sporedni");
            sporedni.appendChild(divMenjaj);
        }
        var gmail = document.querySelector(".InputGmail").value;
        console.log(gmail);
        if(this.isValid(gmail)==true){
        fetch("https://localhost:5001/Spoj/PreuzmiSpoj/"+gmail,{
            method:"GET"
        }).then(p=>{
            if(p.status==200)
                {
                    var div = document.querySelector(".divMenjaj");
                    var change = document.createElement("button");
                    change.innerHTML="Promenite mail";
                    change.className="dugmeMenjaj";//klasa
                    change.onclick=()=>this.Postavka();
                    div.appendChild(change);
                    p.json().then(data=>{
                    data.forEach(el=>{
                        var divTermini = document.querySelector(".divTermini");

                        var divTermin = document.createElement("div");
                        divTermin.className="divTermin"+el.id;
                        divTermin.classList.add("divTermin");
                        divTermini.appendChild(divTermin);

                        console.log(el.ime);
                        console.log(el.prezime);
                        console.log(el.datum);

                        var doca = document.createElement("button");
                        doca.className="btnDoca";
                        doca.innerHTML='<ion-icon name="person-sharp"></ion-icon>';
                        divTermin.appendChild(doca);

                        var ime = document.createElement("label");
                        ime.className="labelTermin";
                        ime.innerHTML=el.ime+" "+el.prezime;
                        divTermin.appendChild(ime);

                        var btnDatum = document.createElement("button");
                        btnDatum.className="btnDatum";
                        btnDatum.innerHTML='<ion-icon name="calendar-sharp"></ion-icon>';
                        divTermin.appendChild(btnDatum);

                        var datum = document.createElement("label");
                        datum.className="labelTermin";
                        datum.innerHTML=el.datum;
                        divTermin.appendChild(datum);

                        var btn = document.createElement("button");
                        btn.className="dugmeObrisi";
                        btn.innerHTML='<ion-icon name="trash"></ion-icon>';
                        btn.value=el.id;
                        divTermin.appendChild(btn);
                        btn.onclick=()=>this.Obrisi(btn.value,el.id);
                        
                    })
                })
            }
            if(p.status==204)
            {
                var dugme = document.querySelector(".dugmeMenjaj");
                if(dugme!=null)
                {
                    var parent = dugme.parentNode;
                    parent.removeChild(dugme);
                }
            }
        })
    }
    }

    Postavka()
    {
        var sporedni = document.querySelector(".divMenjaj");

        var proba1 = document.querySelector(".InputMail");
        var proba2 = document.querySelector(".btnPromeni");
        if(proba1==null && proba2==null){

        var input = document.createElement("input");
        input.className="InputMail";
        input.type="email";
        input.innerHTML="Unesite mail...";
        sporedni.appendChild(input);
        
        var btnPromeni = document.createElement("button");
        btnPromeni.className="btnPromeni";
        btnPromeni.innerHTML='<ion-icon name="checkmark-circle-sharp"></ion-icon>';
        btnPromeni.onclick=()=>this.Menjaj();
        sporedni.appendChild(btnPromeni);
        }
    }
    


    Obrisi(klasa,id)
    {
        fetch("https://localhost:5001/Spoj/ObrisiSpoj/"+id,{
            method:"DELETE"
        }).then(data=>{
            if(data.ok)
            {
                alert("Uspesno Obrisano");
            }
        })
        var s =".divTermin"+klasa;
        var termin = document.querySelector(s);
        var parent = termin.parentNode;
        parent.removeChild(termin);
    }
    
    Menjaj()
    {
        var mail1=document.querySelector(".InputGmail").value;
        var mail2=document.querySelector(".InputMail").value;
        if(mail2==null || mail2 =="")
        {
            alert("Unesite mail!");
        }
        if(this.isValid(mail2)==true)
        {
        fetch("https://localhost:5001/Pacijent/PromeniMail/"+mail1+"/"+mail2,{
            method:"PUT"
        }).then(data=>{
            if(data.ok)
            {
                alert("Zamenili ste mail!");
            }
        })
    }
    }

    isValid(mail)
    {
        if(!/^[a-zA-Z0-9+_.-]+@[a-z]+[.]+[c]+[o]+[m]$/.test(mail))
        {
            alert("Nevalidan unos za e-mail!");
            return false;
        }
        else{
            return true;
        }
    }
}