$(function () {
    // 設定中文語系
    $.datepicker.setDefaults($.datepicker.regional["zh-TW"]);
    $.timepicker.setDefaults($.timepicker.regional["zh-TW"]);

    // 初始化日期選擇器
    $(".datepicker").datepicker({
        //設定允許通過下拉框列表選取月份。
        changeMonth: true,
        //設定允許通過下拉框列表選取年份。
        changeYear: true,
        //是否在面板的頭部年份後面顯示月份
        showMonthAfterYear: true,
        //是否在當前面板顯示上、下兩個月的一些日期數
        showOtherMonths: true,
        //設定是否在面板上顯示相關的按鈕
        showButtonPanel: true,
        //如果設定為true，則約束當前輸入的日期格式。
        constrainInput: true,
        //設定什麼事件觸發顯示日期外掛的面板，可選值：focus, button, both
        showOn: 'focus',
        //設定日期控制元件展開動畫的顯示時間，可選是”slow”, “normal”, “fast”，”代表立刻，數字代表毫秒數。
        duration: 'fast',
        //設定顯示、隱藏日期外掛的動畫的名稱 show, fold
        showAnim: 'fold',
        //如果使用showAnim來顯示動畫效果的話，可以通過此引數來增加一些附加的引數設定
        showOptions: { direction: 'up' },
        //設定日期格式
        dateFormat: 'yy/mm/dd',
        beforeShow: function (input, inst) {
            var $this = $(this);
            var cal = inst.dpDiv;
            var top = $this.offset().top + $this.outerHeight();
            var left = $this.offset().left;

            setTimeout(function () {
                cal.css({
                    'top': top,
                    'left': left
                });
            }, 10);
        }
    });

    // 初始化年度選擇器
    $(".yearpicker").datepicker({
        dateFormat: 'yy',
        changeYear: true,
        showButtonPanel: true,
        closeText: '確定',

        // 關閉時只留下年份
        onClose: function (dateText, inst) {
            var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
            $(this).val(year);
        },

        // 顯示前隱藏月份和日曆
        beforeShow: function (input, inst) {
            setTimeout(function () {
                $('.ui-datepicker-calendar').hide();   // 隱藏日期
                $('.ui-datepicker-month').hide();      // 隱藏月份
                $('.ui-datepicker-close').off('click').on('click', function () {
                    $.datepicker._hideDatepicker();
                });
            }, 0);
        },

        // 年度改變後繼續隱藏不必要內容
        onChangeMonthYear: function (year, month, inst) {
            setTimeout(function () {
                $('.ui-datepicker-calendar').hide();
                $('.ui-datepicker-month').hide();
            }, 0);
        }
    });

    // 初始化年月選擇器
    $(".monthpicker").datepicker({
        dateFormat: 'yy/mm',
        changeMonth: true,
        changeYear: true,
        showButtonPanel: true,

        onClose: function (dateText, inst) {
            var month = inst.selectedMonth + 1;
            var year = inst.selectedYear;
            if (month < 10) month = '0' + month;
            $(this).val(year + '/' + month);
        },

        beforeShow: function (input, inst) {
            setTimeout(function () {
                $('.ui-datepicker-calendar').hide(); // 初始隱藏
                $('.ui-datepicker-close').off('click').on('click', function () {
                    $.datepicker._hideDatepicker();
                });
            }, 0);
        },

        onChangeMonthYear: function (year, month, inst) {
            setTimeout(function () {
                $('.ui-datepicker-calendar').hide(); // 月份或年份變更時隱藏
            }, 0);
        }
    });

    // 初始化年月日時間選擇器
    $(".datetimepicker").datetimepicker({
        dateFormat: 'yy/mm/dd',
        timeFormat: 'HH:mm:ss',
        controlType: 'select',   // 時間用下拉選單
        oneLine: true            // 日期時間在同一行顯示
    });

    $(".datetimeminutepicker").datetimepicker({
        dateFormat: 'yy/mm/dd',
        timeFormat: 'HH:mm',
        controlType: 'select',   // 時間用下拉選單
        oneLine: true            // 日期時間在同一行顯示
    });

    // 初始化年月日時間選擇器
    $(".timepicker").timepicker({
        timeFormat: 'HH:mm:ss',
        controlType: 'select',   // 時間用下拉選單
        oneLine: true            // 日期時間在同一行顯示
    });

    // 初始化年月日時間選擇器
    $(".timeminutepicker").timepicker({
        timeFormat: 'HH:mm',
        controlType: 'select',   // 時間用下拉選單
        oneLine: true            // 日期時間在同一行顯示
    });

    //鍵盤操作：
    //page up/down - 上一月、下一月
    //ctrl page up/down - 上一年、下一年
    //ctrl home - 當前月或最後一次開啟的日期
    //ctrl left/right - 上一天、下一天
    //ctrl up/down - 上一週、下一週
    //enter - 確定選擇日期
    //ctrl end - 關閉並清除已選擇的日期
    //escape - 關閉並取消選擇
    //----------------------------------------------
    //日期格式：
    //d - 每月的第幾天 (沒有前導零)
    //dd - 每月的第幾天(兩位數字)
    //o - 一年中的第幾天(沒有前導零)
    //oo - 一年中的第幾天(三位數字)
    //D - day name short
    //DD - day name long
    //m - 月份(沒有前導零)
    //mm - 月份(兩位數字)
    //M - month name short
    //MM - month name long
    //y - 年份(兩位數字)
    //yy - 年份(四位數字)

    //datetimepicker 參數說明：
    //| 參數 | 說明 |
    //| -------------------- | -------------------------- |
    //| `dateFormat` | 日期格式，例如`yy-mm-dd` |
    //| `timeFormat` | 時間格式，例如 `HH:mm`、`hh:mm tt` |
    //| `controlType` | `slider` 或 `select`（下拉選單）  |
    //| `stepMinute` | 分鐘的步進值（預設 1）               |
    //| `hourMin`, `hourMax` | 限制小時範圍 |
    //| `minDate`, `maxDate` | 限制可選日期範圍 |

});