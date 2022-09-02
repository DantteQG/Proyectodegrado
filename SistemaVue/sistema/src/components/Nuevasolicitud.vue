<template>
    <v-container>
        <v-toolbar flat color="white">
            <v-toolbar-title>Nueva solicitud</v-toolbar-title>
            <v-divider class="mx-2" inset vertical></v-divider>
        </v-toolbar>       
        <template fluid>
            <v-row aling="center">
                <v-toolbar color="blue" dark>Datos Usuario</v-toolbar>     
            </v-row>
            <v-row aling="center" cols="6" sm="1">
                <v-col class="d-inline-flex" >
                    <v-subheader>Usuario:</v-subheader>
                    <v-text-field v-model="nombreusuario" placeholder="Placeholder" outlined disabled ></v-text-field>
                </v-col>
                <v-col class="d-flex">
                    <v-subheader>Area:</v-subheader>
                    <v-select  v-model="idarea" :items="Areas" label="Selecione un area" outlined></v-select>
                </v-col>
                <v-col class="d-flex">
                    <v-subheader>Regional:</v-subheader>
                    <v-select  v-model="idregional" :items="Regionales" label="Selecione una regional" outlined></v-select>
                </v-col>
            </v-row>
            <v-row aling="center" cols="6" sm="1">
                <v-col class="d-flex">
                    <v-subheader>Empresa:</v-subheader>
                    <v-select  v-model="idempresa" :items="Empresas" label="Seleccione una empresa" outlined></v-select>
                </v-col>
                <v-col class="d-flex">
                    <v-subheader>fecha solicitud:</v-subheader>
                    <v-text-field v-model="fechasolicitada"  class="text-xs-center" disabled></v-text-field>
                </v-col>
                <v-col class="d-flex">
                    <v-subheader>Dias:</v-subheader>
                    <v-text-field v-model="diaspago" label="Dias" outlined disabled ></v-text-field>
                </v-col>
            </v-row>
            <v-row aling="center" cols="6" sm="2">
                <v-col class="d-flex">
                    <v-subheader>Tipo de Gasto:</v-subheader>
                    <v-select  v-model="idtipogasto" :items="TipoGastos" label="Seleccione tipo de gasto" @change="Selectdinamic(idtipogasto)" outlined ></v-select>
                </v-col>
                <v-col class="d-flex">
                    <v-subheader>Especificaion de gasto:</v-subheader>
                    <v-select  v-model="idespecifgasto" :items="EspecifGastos" label="Seleccione especificacion" @change="diaespgasto(idespecifgasto)"  outlined></v-select>
                </v-col>
            </v-row>
            <v-row aling="center">
                <v-toolbar color="blue" dark>Datos solicitud</v-toolbar>     
            </v-row>
            <v-row aling="center" >
                <v-col class="d-flex" >
                    <v-subheader>Proyecto:</v-subheader>
                    <v-select  v-model="idproyecto" :items="Proyectos" label="Seleccione proyecto" outlined></v-select>
                </v-col> 
                <v-divider inset vertical></v-divider> 
                <v-col class="d-flex" >
                    <v-subheader>Moneda:</v-subheader>
                    <v-select  v-model="idmoneda" :items="Monedas" label="Seleccione moneda" outlined></v-select>
                </v-col>  
                <v-divider inset vertical></v-divider>
                <v-col class="d-flex">
                    <v-subheader>modo pago:</v-subheader>
                    <v-select  v-model="idmodopago" :items="ModoPagos" label="Seleccione modo pago" outlined></v-select>
                </v-col>      
            </v-row>
            <v-row aling="center">
                <v-col class="d-flex" cols="6" sm="4">
                    <v-subheader>tipo de solicitud:</v-subheader>
                    <v-select  v-model="idtiposolicitud" :items="TipoSolicitudes" label="Seleccione tipo de solicitud" outlined></v-select>
                </v-col> 
                <v-divider inset vertical></v-divider> 
                <v-col class="d-flex" cols="6" sm="3">
                    <label class="mt-5 ml-4">Tiene:</label>
                    <v-spacer></v-spacer>
                    <v-checkbox v-model="factura" color="blue" label="Factura" row></v-checkbox> 
                    <v-spacer></v-spacer>
                    <v-checkbox v-model="recibo" color="blue" label="Recibo"></v-checkbox>   
                </v-col>
            </v-row>
                
            <v-row aling="center">
                <template >
                    <v-toolbar dark color="blue" class="mt-2">
                        <v-toolbar-title>Cuenta destino:</v-toolbar-title>
                        <v-autocomplete v-model="idcuenta" :items="Cuentas" 
                            :search-input.sync="searchcuentas" cache-items
                            class="mx-6" hide-details label="Escriba el nro de cuento o el nombre" solo-inverted>
                        </v-autocomplete>
                    </v-toolbar>   
                </template>    
            </v-row>
            <v-row aling="center">
                <v-textarea v-model="concepto" label="Concepto" auto-grow outlined
                    rows="3" row-height="25" class="mt-4"></v-textarea> 
            </v-row>
            
            <v-container>
                <v-btn color="blue darken-1" @click="agregarDetalle">+</v-btn>
                <v-data-table
                    :headers="cabeceraDetalles"
                    :items="detalles"
                    hide-default-footer
                    class="elevation-2">
                    <template v-slot:[`item.borrar`]="{ item }">
                        <td class=" justify-center layout px-0">
                            <v-icon small class="mr-2" @click="eliminardetalle(detalles,item)">
                                delete
                            </v-icon>
                        </td>
                    </template>

                    <template v-slot:[`item.detalle`]="props">
                        <td><v-text-field v-model="props.item.detalle"></v-text-field></td>
                    </template>
                    <template v-slot:[`item.nrodocumento`]="props">
                        <td><v-text-field v-model="props.item.nrodocumento"></v-text-field></td>
                    </template>
                    <template v-slot:[`item.monto`]="props">
                        <td><v-text-field type="Number" v-model="props.item.monto"></v-text-field></td>
                    </template>
                    <template slot="no-data" black color="red">
                        <strong>DEBE AGREGAR EL DETALLE</strong>
                    </template>
                </v-data-table>
                <v-flex class="text-right mt-2 mr-10">
                    <strong>Total:</strong>{{total=(calcularTotal).toFixed(2)}}
                </v-flex>
            </v-container>
        </template>
       
        <template>
            <v-container class="mt-5">
                <v-row aling="center" cols="6" sm="1">
                    <v-spacer></v-spacer>
                    <v-subheader>Contabilidad:</v-subheader>
                    <v-select class="mr-10" v-model="idcontador" :items="Contadores" label="Selecione un encargado de contabilidad" outlined></v-select>
                </v-row>
                <v-row aling="center" cols="6" sm="1">
                    <v-spacer></v-spacer>
                    <v-subheader>Aprobador:</v-subheader>
                    <v-select class="mr-10" v-model="idaprobador" :items="Aprobadores" label="Selecione un aprobador" outlined></v-select>
                </v-row>
                <v-flex xs12 sm12 md12 v-show="valida">
                    <div class="red--text" v-for="v in validaMensaje" :key="v" v-text="v">
                    </div>
                </v-flex>    
                <v-row aling="center" cols="6" sm="1">
                    <v-spacer></v-spacer>
                    <v-btn class="mt-2" color="blue darken-1" @click="guardar" >Enviar solicitud</v-btn>
                    <v-spacer></v-spacer>
                </v-row>
                <v-dialog v-model="dialog" max-width="500px">
                    <v-card>
                        <v-card-title>
                        <span class="headline">Se ha creado la solicitud: {{ id }}</span>
                        </v-card-title>
        
                        <v-card-text>
                        Contacte con su aprobador
                        </v-card-text>
                        <v-card-actions>
                            <v-spacer></v-spacer>
                            <v-btn  :to="{ name: 'ordendepagos'}" rounded block elevation="10" color="blue" dark class="mb-3">Cerrar</v-btn>
                        </v-card-actions>
                    </v-card>
                </v-dialog>      
            </v-container>
        </template>
        
    </v-container>
</template>

<script>
    import axios from 'axios'
    export default {
        data(){
            return {
                detalles:[],
                dialogdetalle: false,
                cabeceraDetalles: [
                    { text: 'Borrar', value: 'borrar', sortable: false } ,
                    { text: 'Detalle', value: 'detalle' },
                    { text: 'Nro Documento', value: 'nrodocumento' },
                    { text: 'Monto', value: 'monto' },
                ],
                search: '',
                editedIndex: -1,
                id:'',
                Areas:[],
                Regionales:[],
                Empresas:[],
                TipoGastos:[],
                EspecifGastos:[],
                TipoSolicitudes:[],
                Monedas:[],
                ModoPagos:[],
                Proyectos:[],
                Cuentas:[],
                Contadores:[],
                Aprobadores:[],
                valida: 0,
                validaMensaje:[],
                adIdTipoGasto: '',
                idusuario:this.$store.state.usuario.idusuario,
                nombreusuario:this.$store.state.usuario.nombre,
                idarea:'',
                idregional:'',
                idempresa:'',
                idtipogasto:'',
                idespecifgasto:'',
                idtiposolicitud:'',
                idmoneda:'',
                idproyecto:1,
                idmodopago:1,
                factura: false,
                recibo: false,
                idcuenta:'',
                fechasolicitada:new Date().toISOString().split('T')[0],
                concepto:'',
                idaprobador:'',                
                diaspago:'',

                detalle:'',
                nrodocumento:0,
                monto:0,
                total:0,

                searchcuentas: null,

                idaddespgasto:'',
                dialog:false,

            }
        },

        computed: {
            calcularTotal:function(){
                var resultado=0.0;
                for(var i=0;i<this.detalles.length;i++){
                    resultado=resultado+(this.detalles[i].monto*1);
                    //this.detalles[i].monto.push(Number(this.detalles[i].monto));
                }
                return resultado;
            }
        },

        watch: {
            dialog (val) {
            val || this.close()
            }
        },

        created () {
            this.Select();
            this.agregarDetalle();
        },
        methods:{

            Select(){
                let me=this;
                var AreasArray=[];
                axios.get('api/Areas/Select').then(function(response)
                {
                    //console.log(response);
                    AreasArray=response.data;
                    AreasArray.map(function(x){
                        me.Areas.push({text: x.nombre,value:x.idarea});
                    });
                }).catch(function(error){
                    console.log(error);
                });

                var RegionalesArray=[];
                axios.get('api/Regionales/Select').then(function(response)
                {
                    //console.log(response);
                    RegionalesArray=response.data;
                    RegionalesArray.map(function(x){
                        me.Regionales.push({text: x.nombre,value:x.idregional});
                    });
                }).catch(function(error){
                    console.log(error);
                });

                var EmpresasArray=[];
                axios.get('api/Empresas/Select').then(function(response)
                {
                    //console.log(response);
                    EmpresasArray=response.data;
                    EmpresasArray.map(function(x){
                        me.Empresas.push({text: x.nombre,value:x.idempresa});
                    });
                }).catch(function(error){
                    console.log(error);
                });

                var MonedasArray=[];
                axios.get('api/Monedas/Select').then(function(response)
                {
                    //console.log(response);
                    MonedasArray=response.data;
                    MonedasArray.map(function(x){
                        me.Monedas.push({text: x.nombre,value:x.idmoneda});
                    });
                }).catch(function(error){
                    console.log(error);
                });

                var ModoPagosArray=[];
                axios.get('api/Modopagos/Select').then(function(response)
                {
                    //console.log(response);
                    ModoPagosArray=response.data;
                    ModoPagosArray.map(function(x){
                        me.ModoPagos.push({text: x.nombre,value:x.idmodopago});
                    });
                }).catch(function(error){
                    console.log(error);
                });
                var ProyectosArray=[];
                axios.get('api/Proyectos/Select').then(function(response)
                {
                    //console.log(response);
                    ProyectosArray=response.data;
                    ProyectosArray.map(function(x){
                        me.Proyectos.push({text: x.nombre,value:x.idproyecto});
                    });
                }).catch(function(error){
                    console.log(error);
                });
                var TipoSolicitudesArray=[];
                axios.get('api/Tiposolicitudes/Select').then(function(response)
                {
                    //console.log(response);
                    TipoSolicitudesArray=response.data;
                    TipoSolicitudesArray.map(function(x){
                        me.TipoSolicitudes.push({text: x.nombre,value:x.idtiposolicitud});
                    });
                }).catch(function(error){
                    console.log(error);
                });
                var TipoGastosArray=[];
                axios.get('api/TipoGastos/Select').then(function(response)
                {
                    //console.log(response);
                    TipoGastosArray=response.data;
                    TipoGastosArray.map(function(x){
                        me.TipoGastos.push({text: x.nombre,value:x.idtipogasto});
                    });
                }).catch(function(error){
                    console.log(error);
                });

                var CuentasArray=[];
         
                axios.get('api/Cuentas/Select').then(function(response)
                {
                    //console.log(response);
                    CuentasArray=response.data;
                    CuentasArray.map(function(x){
                        me.Cuentas.push({text: x.cuenta+' - '+x.nombre+' - '+x.banco,value:x.idcuenta});
                    });
                    //me.codigobanco=Cuentas.text2;
                }).catch(function(error){
                    console.log(error);
                });

                var AprobadoresArray=[];        
                axios.get('api/Usuarios/Selectaprobador').then(function(response)
                {
                    //console.log(response);
                    AprobadoresArray=response.data;
                    AprobadoresArray.map(function(x){
                        me.Aprobadores.push({text: x.nombre,value:x.idusuario});
                    });
                    //console.log(me.Aprobadores)
                }).catch(function(error){
                    console.log(error);
                });
                var ContadoresArray=[];        
                axios.get('api/Usuarios/Selectcontador').then(function(response)
                {
                    //console.log(response);
                    ContadoresArray=response.data;
                    ContadoresArray.map(function(x){
                        me.Contadores.push({text: x.nombre,value:x.idusuario});
                    });
                    //console.log(me.Aprobadores)
                }).catch(function(error){
                    console.log(error);
                });
                     
            },

            Selectdinamic(idtipogasto){
                let me=this;
                me.diaspago='';
                me.idespecifgasto='';
                me.EspecifGastos=[];
                //me.value='',
                this.adIdTipoGasto=idtipogasto;
                var EspecifGastosArray=[];
                axios.get('api/Especifgastos/Select/'+this.adIdTipoGasto,{}).then(function(response)
                { 
                    //console.log(response);
                    EspecifGastosArray=response.data;
                    EspecifGastosArray.map(function(x){
                        me.EspecifGastos.push({text: x.nombre,value:x.idespecifgasto});
                    });
                }).catch(function(error){
                    console.log(error);
                });
            },

            diaespgasto(idespgasto){
                let me=this;
                this.idaddespgasto=idespgasto;
                var EspecifdiaArray=[];
                axios.get('api/Especifgastos/mostrar/'+this.idaddespgasto,{}).then(function(response)
                { 
                    //console.log(response);
                    EspecifdiaArray=response.data;
                    me.diaspago=EspecifdiaArray.dias;
                }).catch(function(error){
                    console.log(error);
                });
            },

           agregarDetalle(){
               this.detalles.push(
                   {detalle: "",nrodocumento:"",monto:0
                   }
               );
            },

            eliminardetalle(arr,item){
                var i=arr.indexOf(item);
                if(i!==-1){
                    arr.splice(i,1)
                }
            },

            close(){
                
                this.dialog = false
            },
            convertirnumero(){
                for(var i=0;i<this.detalles.length;i++){
                  this.detalles[i].monto=(this.detalles[i].monto)*1;
                }
            },


            guardar(){
                this.convertirnumero();
                if(this.validar()){
                   return;
                }
                
                let me=this;
                me.total=me.total*1;
                
                axios.post('api/Ordendepagos/Crear',{
                    'idusuario':me.idusuario,
                    'idaprobador':me.idaprobador,
                    'idcontador':me.idcontador,
                    'idregional':me.idregional,
                    'idarea':me.idarea,
                    'idempresa':me.idempresa,
                    'idespecifgasto':me.idespecifgasto,
                    'idmoneda':me.idmoneda,
                    'idtiposolicitud':me.idtiposolicitud,
                    'idproyecto':me.idproyecto,
                    'idmodopago':me.idmodopago,
                    'idcuenta':me.idcuenta,
                    'dias':me.diaspago,
                    'factura':me.factura,
                    'recibo':me.recibo,
                    'concepto':me.concepto,
                    'total':me.total,
                    'detalleorden':me.detalles

                }).then(function(response){ 
                    me.id=response.data.id;
                    me.dialog=true;          
                }).catch(function(error){
                    console.log(error);
                })
            
             
            },

            validar(){
                this.valida=0;
                this.validaMensaje=[];
                if(!this.idarea){
                    this.validaMensaje.push("Seleccione un area.")
                }
                if(!this.idregional){
                    this.validaMensaje.push("Seleccione un regional.")
                }
                if(!this.idempresa){
                    this.validaMensaje.push("Seleccione una empresa.")
                }
                if(!this.idtipogasto){
                    this.validaMensaje.push("Seleccione un tipo de gasto.")
                }
                if(!this.idespecifgasto){
                    this.validaMensaje.push("Seleccione una especificacion de gasto.")
                }
                if(!this.idtiposolicitud){
                    this.validaMensaje.push("Seleccione un tipo de solicitud.")
                }
                if(!this.idmoneda){
                    this.validaMensaje.push("Seleccione moneda.")
                }
                if(!this.idmodopago){
                    this.validaMensaje.push("Seleccione un modo de pago.")
                }
                if(!this.idcontador){
                    this.validaMensaje.push("Seleccione un modo de pago.")
                }
                if(!this.idproyecto){
                    this.validaMensaje.push("Seleccione un modo de pago.")
                }
                if(!this.idcuenta){
                    this.validaMensaje.push("Seleccione una cuenta.")
                }
                if(this.concepto.length<3 || this.concepto.length>256){
                    this.validaMensaje.push("Escriba el concepto en menos de 256 caracteres.")
                }
                if(!this.idaprobador){
                    this.validaMensaje.push("Seleccione un aprobador.")
                }
                
                for(var i=0;i<this.detalles.length;i++){
                    if(this.detalles[i].monto==0){
                        this.validaMensaje.push("los importes no deben ser igual a 0.");
                    }
                }
                for(var i=0;i<this.detalles.length;i++){
                    if(this.detalles[i].detalle==""){
                        this.validaMensaje.push("falta detalle(s).");
                    }
                }

                if(this.validaMensaje.length){
                    this.valida=1;
                }
               
                return this.valida;
            }

            

        }        
    }
</script>

    